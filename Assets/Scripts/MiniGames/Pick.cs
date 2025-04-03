using UnityEngine;

namespace LOCKPICKING
{
    public class Pick : MonoBehaviour
    {
        [Header("Настройка отмычки")]
        private Camera _camera;
        private Lock _lock;
        private float _eulerAngle = 0f;

        private bool _isMoving = true;
        public bool IsMoving => _isMoving;
        public float Angle => _eulerAngle;

        public void Init(Camera camera, Lock lockImpl)
        {
            _camera = camera;
            _lock = lockImpl;
            _lock.Unlocked.AddListener(ResetMovement);
        }

        public void OnDestroy()
        {
            if (_lock)
                _lock.Unlocked.RemoveListener(ResetMovement);
        }

        private void Update()
        {
            transform.localPosition = _lock.PickAnchorPosition;

            if (_isMoving)
            {
                var maxAngle = _lock.MaxRotationAngle;

                _eulerAngle += (-Input.GetAxis("Mouse X")*20 -Input.GetAxis("Mouse Y")*15) * Time.deltaTime;

                _eulerAngle = Mathf.Clamp(_eulerAngle, -maxAngle, maxAngle);

                var targetRotation = Quaternion.AngleAxis(_eulerAngle, Vector3.forward);
                transform.localRotation = targetRotation;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                _isMoving = false;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                _isMoving = true;
            }
        }

        private void ResetMovement()
        {
            _isMoving = true;
        }
    }
}