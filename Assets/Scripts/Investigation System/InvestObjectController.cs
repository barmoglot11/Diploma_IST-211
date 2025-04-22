using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace INVESTIGATION
{
    [RequireComponent(typeof(MeshRenderer))]
    public class InvestObjectController : MonoBehaviour
    {
        [Header("Materials")]
        [SerializeField] private Material _investigationMaterial;
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private List<ParticleSystem> _particleSystems;
        
        [Header("Settings")]
        [SerializeField] private float _fadeDuration = 1f;
        [SerializeField] private bool _useSecondaryMaterial = true;
        
        [Header("Optional")]
        [SerializeField] private GameObject _questChanger;
        
        [Header("Debug")]
        [SerializeField] private bool _debugLogs = true;

        private MeshRenderer _meshRenderer;
        private Material _currentMaterial;
        private Coroutine _fadeCoroutine;
        private bool _isVisible;
        private int _targetMaterialIndex;

        private bool IsInvestigating => InvestigationManager.Instance != null && 
                                     InvestigationManager.Instance.IsInvestigating;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _targetMaterialIndex = _useSecondaryMaterial ? 1 : 0;
            CacheCurrentMaterial();
        }

        private void OnEnable()
        {
            UpdateMaterialState(immediate: true);
        }

        private void OnDisable()
        {
            if (_fadeCoroutine != null)
            {
                StopCoroutine(_fadeCoroutine);
                _fadeCoroutine = null;
            }
        }

        private void CacheCurrentMaterial()
        {
            _currentMaterial = new Material(
                _meshRenderer.materials.Length > _targetMaterialIndex 
                    ? _meshRenderer.materials[_targetMaterialIndex] 
                    : _meshRenderer.material
            );
        }

        private void Update()
        {
            if (_isVisible != IsInvestigating)
            {
                UpdateMaterialState();
                
            }
        }

        private void UpdateMaterialState(bool immediate = false)
        {
            if (_fadeCoroutine != null)
            {
                StopCoroutine(_fadeCoroutine);
            }

            _isVisible = IsInvestigating;
            var targetMaterial = _isVisible ? _investigationMaterial : _defaultMaterial;

            if (immediate || Mathf.Approximately(_fadeDuration, 0f))
            {
                ApplyMaterialImmediately(targetMaterial);
            }
            else
            {
                _fadeCoroutine = StartCoroutine(FadeMaterial(targetMaterial));
            }
            ChangeParticleStatus();
            UpdateQuestChanger();
        }

        void ChangeParticleStatus()
        {
            foreach (var particle in _particleSystems)
            {
                if(IsInvestigating && !particle.isPlaying)
                    particle.Play();
                if(!IsInvestigating && particle.isPlaying)
                    particle.Stop();
            }
        }
        
        private void ApplyMaterialImmediately(Material material)
        {
            if (_meshRenderer.materials.Length > _targetMaterialIndex)
            {
                var materials = _meshRenderer.materials;
                materials[_targetMaterialIndex] = material;
                _meshRenderer.materials = materials;
            }
            else
            {
                _meshRenderer.material = material;
            }

            if (_debugLogs)
            {
                Debug.Log($"[InvestObject] Material changed immediately to {(IsInvestigating ? "investigation" : "default")} mode", this);
            }
        }

        private IEnumerator FadeMaterial(Material targetMaterial)
        {
            float elapsedTime = 0f;
            Material tempMaterial = new Material(_currentMaterial);

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
        }

        private void LerpMaterial(Material result, Material start, Material end, float t)
        {
            result.color = Color.Lerp(start.color, end.color, t);
            // Добавьте здесь другие свойства материала, которые нужно интерполировать
        }

        private void ApplyTemporaryMaterial(Material material)
        {
            if (_meshRenderer.materials.Length > _targetMaterialIndex)
            {
                var materials = _meshRenderer.materials;
                materials[_targetMaterialIndex] = material;
                _meshRenderer.materials = materials;
            }
            else
            {
                _meshRenderer.material = material;
            }
        }

        private void UpdateQuestChanger()
        {
            if (_questChanger != null)
            {
                _questChanger.SetActive(!IsInvestigating);
                if (_debugLogs)
                {
                    Debug.Log($"[InvestObject] QuestChanger {(_questChanger.activeSelf ? "activated" : "deactivated")}", this);
                }

                _questChanger = null;
            }
        }
    }
}