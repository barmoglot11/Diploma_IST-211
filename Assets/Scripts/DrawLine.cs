using System;
using UnityEngine;

namespace BATTLE
{
    public class DrawLine : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        private Vector3 startPos;
        private Vector3 endPos;
        public Transform muzzleTransform;
        private void Start()
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.blue;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.positionCount = 2;
        }

        public void ApplyDrawLine(Vector3 localStartPos, Vector3 localEndPos)
        {
            startPos = muzzleTransform.TransformPoint(localStartPos);
            endPos = muzzleTransform.TransformPoint(localEndPos);
        }

        private void Update()
        {
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, endPos);

            // Вычисляем направление линии
            Vector3 direction = endPos - startPos;

            // Поворачиваем объект, чтобы он смотрел на линию
            if (direction != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(direction, muzzleTransform.up);
                transform.rotation = rotation;
            }
        }

    }
}