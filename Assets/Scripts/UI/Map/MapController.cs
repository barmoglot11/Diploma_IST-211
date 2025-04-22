using UnityEngine;
using UnityEngine.UI;

namespace MAP
{
    public class MapController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Camera _mapCamera;
        [SerializeField] private Slider _zoomSlider;

        [Header("Zoom Settings")]
        [SerializeField] private float _maxZoomScale = 30f;
        [SerializeField] private float _minZoomScale = 5f;
        [SerializeField] private float _zoomSensitivity = 1f;

        private void Awake()
        {
            ValidateReferences();
            InitializeZoomSlider();
        }

        private void OnEnable()
        {
            _zoomSlider.onValueChanged.AddListener(HandleZoomChanged);
        }

        private void OnDisable()
        {
            _zoomSlider.onValueChanged.RemoveListener(HandleZoomChanged);
        }

        private void ValidateReferences()
        {
            if (_mapCamera == null)
            {
                Debug.LogError($"{nameof(MapController)}: Map Camera is not assigned!", this);
                enabled = false;
            }

            if (_zoomSlider == null)
            {
                Debug.LogError($"{nameof(MapController)}: Zoom Slider is not assigned!", this);
                enabled = false;
            }
        }

        private void InitializeZoomSlider()
        {
            _zoomSlider.minValue = _minZoomScale / _maxZoomScale;
            _zoomSlider.maxValue = 1f;
            _zoomSlider.value = 1f;
            UpdateCameraZoom(_zoomSlider.value);
        }

        private void HandleZoomChanged(float sliderValue)
        {
            UpdateCameraZoom(sliderValue);
        }

        private void UpdateCameraZoom(float sliderValue)
        {
            float zoomLevel = Mathf.Lerp(_minZoomScale, _maxZoomScale, sliderValue);
            _mapCamera.orthographicSize = zoomLevel;
        }

        // Optional: Add mouse wheel zoom support
        private void Update()
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0f)
            {
                float zoomDelta = scrollInput * _zoomSensitivity;
                _zoomSlider.value = Mathf.Clamp01(_zoomSlider.value + zoomDelta);
            }
        }
    }
}