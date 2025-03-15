using UnityEngine;

namespace UI
{
    public class SpriteToCamera : MonoBehaviour
    {
        [SerializeField] private Camera camera;

        private void LateUpdate()
        {
            Vector3 cameraPos = camera.transform.position;
            cameraPos.y = transform.position.y;
            transform.LookAt(cameraPos);
            transform.Rotate(0f, 180f, 0f);
        }
    }
}