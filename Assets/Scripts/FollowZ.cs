using UnityEngine;

public class LightZFollow : MonoBehaviour
{
    public Transform player;
    [SerializeField]private Vector3 offset;

    void LateUpdate()
    {
        Vector3 targetPos = player.position + offset;

        // Обновляем только X и Z, оставляя Y как есть (например, 90)
        transform.position = new Vector3(targetPos.x, transform.position.y, targetPos.z);
    }
}