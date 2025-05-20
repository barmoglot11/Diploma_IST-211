using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BATTLE
{
    [RequireComponent(typeof(AudioSource))]
    public class Gun : MonoBehaviour
    {
        [Header("Gun Settings")]
        [SerializeField] private MainCharacter _mainCharacter;
        [SerializeField] private AudioClip _shotSound;
        [SerializeField] private AudioClip _equipSound;
        [SerializeField] private Transform _shotOrigin;
        [SerializeField] private float _cooldown = 1f;
        [SerializeField] private int _maxAmmo = 2;
        [SerializeField] private float _shotRange = 10f;
        [SerializeField] private LayerMask _shotLayerMask;
        [SerializeField]private ParticleEffectObject _shotParticle;
        //[SerializeField] private DrawLine _drawLine;

        private AudioSource _audioSource;
        private int _currentAmmo;
        private bool _isCoolingDown;
        private bool _isEquipped;
        

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _currentAmmo = _maxAmmo;
        }
        private void OnEnable() => Equip();

        private void OnDisable() => Unequip();

        public void LinkInput()
        {
            _mainCharacter.SetupSnipeControls(this);
        }
        
        public void UnlinkInput()
        {
            _mainCharacter.UnlinkSnipeControls(this);
        }
        
        public void Equip()
        {
            if (!_isEquipped)
            {
                _audioSource.PlayOneShot(_equipSound);
                _isEquipped = true;
            }
        }

        public void Unequip()
        {
            if (_isEquipped)
            {
                _audioSource.PlayOneShot(_equipSound);
                _isEquipped = false;
            }
        }

        public void Shoot()
        {
            if (CanShoot())
            {
                ProcessShot();
                _currentAmmo--;
                
                if (_currentAmmo <= 0)
                {
                    StartCoroutine(ReloadRoutine());
                }
            }
        }

        private bool CanShoot()
        {
            if (_isCoolingDown)
            {
                Debug.Log("Cooldown in progress. Please wait.");
                return false;
            }

            if (_currentAmmo <= 0)
            {
                Debug.Log("Out of ammo. Reload required.");
                return false;
            }

            return true;
        }

        private void ProcessShot()
        {
            _audioSource.PlayOneShot(_shotSound);
            
            _shotParticle?.Play();

            for (var i = 0; i < 30; i++){
                if (ShootCone(out var hit, 25f))
                {
                    if (hit.collider.TryGetComponent<Enemy>(out var enemy))
                    {
                        enemy.Stagger();
                    }
                }
            }

            
        }

        private bool ShootCone(out RaycastHit hit, float spreadAngle)
        {
            Vector3 direction = RandomConeDirection(-_shotOrigin.forward, spreadAngle);
            
            Ray ray = new Ray(_shotOrigin.position, direction);
            Debug.DrawRay(ray.origin, ray.direction*_shotRange,
                _currentAmmo > 0 ? Color.green : Color.red);
            
            return Physics.Raycast(ray, out hit, _shotRange, _shotLayerMask);

        }
        
        private Vector3 RandomConeDirection(Vector3 forward, float angle)
        {
            float spread = Mathf.Tan(Mathf.Deg2Rad * angle / 2);
            Vector2 randomCircle = Random.insideUnitCircle * spread;
            return Quaternion.LookRotation(forward) * new Vector3(randomCircle.x, randomCircle.y, 1f).normalized;
        }

        private IEnumerator ReloadRoutine()
        {
            _isCoolingDown = true;
            Debug.Log("Reloading...");

            yield return new WaitForSeconds(_cooldown);

            _currentAmmo = _maxAmmo;
            _isCoolingDown = false;
            Debug.Log("Reload complete. Ammo restored.");
        }
    }
}