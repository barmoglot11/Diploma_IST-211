using System;
using System.Collections;
using UnityEngine;

namespace BATTLE
{
    public class Gun : MonoBehaviour
    {
        [Header("Настройки пистолета")]
        [SerializeField]
        MainCharacter MC;
        AudioSource Source => GetComponent<AudioSource>();
        public AudioClip shotSound;
        public AudioClip getSound;
        
        public Transform shotTransform;
        public float cooldown = 1f;
        public int currentAmmo = 2;
        private bool isCoolingDown = false;
        private Vector3 forvatdDir = -Vector3.forward;
        private RaycastHit hit;
        //TODO: Пускать raycast из пистолета на расстояние N и проверять попадание - вызвать стаггер

        public void OnEnable()
        {
            Source.PlayOneShot(getSound);
        }
        public void OnDisable()
        {
            Source.PlayOneShot(getSound);
        }
        private void FixedUpdate()
        {
                
            if (Physics.Raycast(shotTransform.position, transform.TransformDirection(forvatdDir), out hit, 10))
            { 
                Debug.DrawRay(shotTransform.position, transform.TransformDirection(forvatdDir) * hit.distance, Color.green); 
            }
            else
            { 
                Debug.DrawRay(shotTransform.position, transform.TransformDirection(forvatdDir) * 10, Color.red); 
            }
        }

        public void Shot()
        {
            if (currentAmmo > 0)
            {
                if (!isCoolingDown)
                {
                    if (Physics.Raycast(transform.position, transform.TransformDirection(forvatdDir), out hit, 10))
                    {
                        Source.PlayOneShot(shotSound);
                        if (hit.collider.gameObject.TryGetComponent<Enemy>(out var enemy))
                        {
                            enemy.Stagger();
                        }
                    }
                    
                    if (currentAmmo == 0)
                    {
                        StartCoroutine(Reload());
                    }
                }
                else
                {
                    Debug.Log("Перезарядка в процессе. Подождите.");
                }
            }
            else
            {
                Debug.Log("Патроны закончились. Перезарядка необходима.");
            }
        }

        private IEnumerator Reload()
        {
            isCoolingDown = true;
            Debug.Log("Перезарядка...");
    
            // Ждем время перезарядки
            yield return new WaitForSeconds(cooldown);
    
            // Восстанавливаем патроны
            currentAmmo = 2;
            isCoolingDown = false;
            Debug.Log("Перезарядка завершена. Патроны восстановлены.");
        }
    }
}