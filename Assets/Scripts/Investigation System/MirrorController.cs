using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace INVESTIGATION
{
    public class MirrorController : MonoBehaviour
    {
        private static MirrorController _instance;
        public static MirrorController Instance => _instance;

        [Header("Materials")]
        [SerializeField]private Material _mirrorMaterialDefault;
        [SerializeField]private Material _mirrorMaterialInvestigation;
        [SerializeField]private List<Material> _mirrorMaterials;

        [Header("Settings")]
        [SerializeField]private float _fadeDuration = 1f;
        [SerializeField]private bool _isInvestMaterial;
        [SerializeField]private MeshRenderer _meshRenderer;
        
        [Header("Debug")]
        private bool _debugLogs;
        
        private Coroutine _transitionCoroutine;
        private Coroutine _fadeCoroutine;
        private Material _currentMaterial;
        
        private bool IsInvestigating => InvestigationManager.Instance != null && InvestigationManager.Instance.IsInvestigating;

        #region Public Methods

        public void SetMaterial(int index = -1)
        {
            if(index == -1)
            {
                ChangeMaterial();
            }
            else
            {
                ChangeMaterial(_mirrorMaterials[index]);
            }
        }

        #endregion
        
        #region Private Methods

        private void ChangeMaterial(Material targetMaterial = null)
        {
            if(_transitionCoroutine != null)
                StopCoroutine(_transitionCoroutine);
            
            _isInvestMaterial = IsInvestigating;

            if (Mathf.Approximately(_fadeDuration, 0f))
            {
                ApplyMaterialImmediately(targetMaterial);
            }
            else
            {
                if(targetMaterial != null)
                {
                    _transitionCoroutine = StartCoroutine(FadeMaterial(targetMaterial));
                }
                else
                {
                    _transitionCoroutine = StartCoroutine(TransitionMaterial(_isInvestMaterial ? _mirrorMaterialInvestigation : _mirrorMaterialDefault));
                }
            }
        }

        private IEnumerator TransitionMaterial(Material targetMaterial)
        {
            _fadeCoroutine = StartCoroutine(FadeMaterial(_mirrorMaterialDefault));
            while (_fadeCoroutine != null)
                yield return null;
            _fadeCoroutine = StartCoroutine(FadeMaterial(targetMaterial));
        }

        private IEnumerator FadeMaterial(Material targetMaterial)
        {
            var elapsedTime = 0f;
            var tempMaterial = new Material(_currentMaterial);

            while (elapsedTime < _fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float lerpFactor = elapsedTime / _fadeDuration;
                
                LerpMaterial(tempMaterial, _currentMaterial, targetMaterial, lerpFactor);
                ApplyTemporaryMaterial(tempMaterial);
                
                yield return null;
            }
            ApplyMaterialImmediately(targetMaterial);
            _currentMaterial = new Material(targetMaterial);
            _fadeCoroutine = null;
            yield return null;
        }
        
        private void LerpMaterial(Material result, Material start, Material end, float t)
        {
            result.color = Color.Lerp(start.color, end.color, t);
            // Добавьте здесь другие свойства материала, которые нужно интерполировать
        }
        
        private void ApplyTemporaryMaterial(Material material)=> _meshRenderer.material = material;
        
        private void ApplyMaterialImmediately(Material material)
        {
             _meshRenderer.material = material;

            if (_debugLogs)
            {
                Debug.Log($"[InvestObject] Material changed immediately to {(IsInvestigating ? "investigation" : "default")} mode", this);
            }
        }
        
        #endregion
        
        #region Special Methods

        private void Awake()
        {
            Initialize();
            OnValidate();
            CacheCurrentMaterial();
        }
        
        private void Initialize()
        {
            if (Instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnValidate()
        {
            if(_mirrorMaterialDefault == null) Debug.LogError("Default mirror Material is null!");
            if(_mirrorMaterialInvestigation == null) Debug.LogError("Investigation mirror Material is null!");
        }

        private void CacheCurrentMaterial()
        {
            _currentMaterial = new Material(_meshRenderer.material);
        }

        private void OnDisable()
        {
            if(_fadeCoroutine != null)
            {
                StopCoroutine(_fadeCoroutine);
                _fadeCoroutine = null;
            }
        }

        #endregion
       
    }
}