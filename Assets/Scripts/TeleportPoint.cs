using UnityEngine;


public class TeleportPoint : MonoBehaviour
{
    public MainCharacter Character;
    public GameObject Point;

    public void Teleport()
    {
        Character.gameObject.transform.position = Point.transform.position;
    }
}
