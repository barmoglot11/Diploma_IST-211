using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace INVESTIGATION
{
    public class InvestObjectController : MonoBehaviour
    {
        public MeshRenderer ObjMeshRenderer;
        public float fadeDuration = 1f;
        private Coroutine process; 
        public bool isVisible = false;
        public Material[] Materials;
        public Material currentMaterial;
        private bool Investigating => InvestigationManager.Instance.Investigating;

        public GameObject QuestChanger = null;
        
        private void Start()
        {
            currentMaterial = ObjMeshRenderer.materials.Length >= 2 ? ObjMeshRenderer.materials[1] : ObjMeshRenderer.material;
        }

        public void Update()
        {
            if (!isVisible && Investigating)
            {
                if(process != null)
                    StopCoroutine(process);
                currentMaterial = ObjMeshRenderer.materials.Length >= 2 ? ObjMeshRenderer.materials[1] : ObjMeshRenderer.material;
                process = StartCoroutine(FadeObject(Materials[0]));
                if(QuestChanger != null)
                    QuestChanger.SetActive(false);
            }
               
            if (isVisible && !Investigating)
            {
                if(process != null)
                    StopCoroutine(process);
                currentMaterial = ObjMeshRenderer.materials.Length >= 2 ? ObjMeshRenderer.materials[1] : ObjMeshRenderer.material;
                process = StartCoroutine(FadeObject(Materials[1]));
  
            }
            
        }
        
        private IEnumerator FadeObject(Material targetMaterial)
        {
            Material material = new Material(currentMaterial);
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                material.Lerp(material, targetMaterial, elapsedTime / fadeDuration);
                if (ObjMeshRenderer.materials.Length >= 2)
                    ObjMeshRenderer.materials[1] = material;
                else
                    ObjMeshRenderer.material = material;
                yield return null;
            }
            
            if (ObjMeshRenderer.materials.Length >= 2)
                ObjMeshRenderer.materials[1] = targetMaterial;
            else
                ObjMeshRenderer.material = targetMaterial;

            isVisible = !isVisible;
        }
    }
}