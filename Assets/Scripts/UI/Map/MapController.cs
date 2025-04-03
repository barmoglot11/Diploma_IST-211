using System;
using UnityEngine;
using UnityEngine.UI;

namespace MAP
{
    public class MapController : MonoBehaviour
    {
        [SerializeField]
        Camera cam;
        [SerializeField]
        Slider zoomSlide;
        public int zoomScale = 30;
        //TODO
        //[ ]Боевая система(проверка пушки),
        //[x]бестиарий,
        //[x]нормально инвентарь почистить,
        //[x]тп локаций,
        //[-]расследование полупрозрачностью

        private void Update()
        {
            cam.orthographicSize = Mathf.Clamp(zoomSlide.value * zoomScale, 5, zoomScale);
        }
    }
}