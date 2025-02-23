using UnityEngine;

namespace LOCKPICKING
{
    public class Pick : MonoBehaviour
    {
        private Camera _camera;
        private Lock _lock;
        private float _eulerAngle;

        private bool _isMoving = true;
        public bool IsMoving => _isMoving;
        public float Angle => _eulerAngle;

        public void Init(Camera camera, Lock lockImpl)
        {
            _camera = camera;
            _lock = lockImpl;
            _lock.Unlocked += ResetMovement;
        }

        public void OnDestroy()
        {
            if (_lock)
                _lock.Unlocked -= ResetMovement;
        }

        private void Update()
        {
            transform.localPosition = _lock.PickAnchorPosition;

            if (_isMoving)
            {
                var maxAngle = _lock.MaxRotationAngle;
                var direction = Input.mousePosition - _camera.WorldToScreenPoint(transform.position);

                _eulerAngle = Vector3.Angle(direction, Vector3.up);

                var cross = Vector3.Cross(Vector3.up, direction);
                if (cross.z < 0)
                    _eulerAngle = -_eulerAngle;

                _eulerAngle = Mathf.Clamp(_eulerAngle, -maxAngle, maxAngle);

                var targetRotation = Quaternion.AngleAxis(_eulerAngle, Vector3.forward);
                transform.rotation = targetRotation;
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