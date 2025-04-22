using UnityEngine;
using UnityEngine.UI;

namespace INVENTORY
{
    public class PreviewScreen : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Transform _itemContainer;
        [SerializeField] private Button _returnButton;
        [SerializeField] private float _itemScale = 200f;
        [SerializeField] private Vector3 _itemRotation = new Vector3(0f, -90f, 0f);

        [Header("Animation")]
        [SerializeField] private float _fadeDuration = 0.3f;
        [SerializeField] private CanvasGroup _canvasGroup;

        private GameObject _currentPreviewItem;
        private bool _isInitialized;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (_isInitialized) return;

            if (_returnButton != null)
            {
                _returnButton.onClick.AddListener(ClosePreview);
            }
            else
            {
                Debug.LogError("Return button reference is missing!", this);
            }

            if (_canvasGroup != null)
            {
                _canvasGroup.alpha = 0f;
                _canvasGroup.blocksRaycasts = false;
            }

            _isInitialized = true;
        }

        public void CreateItem(GameObject itemPrefab)
        {
            if (itemPrefab == null)
            {
                Debug.LogWarning("Attempted to create preview with null prefab", this);
                return;
            }

            ClearCurrentItem();

            try
            {
                _currentPreviewItem = Instantiate(itemPrefab, _itemContainer);
                ConfigurePreviewItem(_currentPreviewItem);
                ShowPanel();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to create preview item: {e.Message}", this);
                ClearCurrentItem();
            }
        }

        private void ConfigurePreviewItem(GameObject item)
        {
            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = Vector3.one * _itemScale;
            item.transform.localEulerAngles = _itemRotation;

            // Disable physics/colliders if they exist
            var colliders = item.GetComponentsInChildren<Collider>();
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }

            var rigidbodies = item.GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = true;
            }
        }

        public void ShowPanel()
        {
            if (_canvasGroup != null)
            {
                _canvasGroup.blocksRaycasts = true;
                LeanTween.alphaCanvas(_canvasGroup, 1f, _fadeDuration)
                    .setEase(LeanTweenType.easeInOutQuad);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }

        public void ClosePreview()
        {
            if (_canvasGroup != null)
            {
                _canvasGroup.blocksRaycasts = false;
                LeanTween.alphaCanvas(_canvasGroup, 0f, _fadeDuration)
                    .setEase(LeanTweenType.easeInOutQuad)
                    .setOnComplete(ClearCurrentItem);
            }
            else
            {
                ClearCurrentItem();
                gameObject.SetActive(false);
            }
        }

        public void ClearCurrentItem()
        {
            if (_currentPreviewItem != null)
            {
                Destroy(_currentPreviewItem);
                _currentPreviewItem = null;
            }
        }

        private void OnDestroy()
        {
            if (_returnButton != null)
            {
                _returnButton.onClick.RemoveListener(ClosePreview);
            }
        }
    }
}