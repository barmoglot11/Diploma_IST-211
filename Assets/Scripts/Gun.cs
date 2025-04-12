using System.Collections;
using UnityEngine;

namespace BATTLE
{
    public class Gun : MonoBehaviour
    {
        [Header("Настройки пистолета")]
        public Transform shotTransform;
        public GameObject bulletPrefab;
        public float bulletSpeed = 20f;
        public float cooldown = 1f;
    
        public int currentAmmo = 2;
        private bool isCoolingDown = false;
    
        //TODO: Пускать raycast из пистолета на расстояние N и проверять попадание - вызвать стаггер
        
        public void Shot()
        {
            if (currentAmmo > 0)
            {
                if (!isCoolingDown)
                {
                    var bullet = Instantiate(bulletPrefab, shotTransform);
                    bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
                    
                    currentAmmo--;
                    Debug.Log("Выстрел! Осталось патронов: " + currentAmmo);
                    
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

        public void Snipe()
        {
            
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