using Unity.Mathematics;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public GameObject lockPrefab;

    GameObject a;
    Camera camera;
    FirstPersonController player;
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent<mechanic>(out mechanic component))
        {
            player = other.gameObject.GetComponent<FirstPersonController>();
            player.playerCanMove = false;
            camera = Camera.main;
            camera.gameObject.SetActive(false);
            StartPicking(other.gameObject.transform.position);
        }
    }
    
    void StartPicking(Vector3 position )
    {
        a = Instantiate(lockPrefab.gameObject, position + new Vector3(0,3,0), Quaternion.identity);
        a.GetComponentInChildren<LockPick>().lockIsDone += StopPicking;
        a.GetComponentInChildren<LockPick>().lockIsDone += PlayAnimationMode;

    }

    void StopPicking()
    {
        player.playerCanMove = true;

        camera.gameObject.SetActive(true);
        Destroy(a);
    }

    void PlayAnimationMode()
    {
        
    }
}
