using System.Collections;
using UnityEngine;

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

        private void OnEnable()
        {
            Equip();
        }

        private void OnDisable()
        {
            Unequip();
        }

        private void Update()
        {
            Debug.DrawRay(_shotOrigin.position, -_shotOrigin.forward * _shotRange, 
                         _currentAmmo > 0 ? Color.green : Color.red);
            //_drawLine.ApplyDrawLine(_shotOrigin.position, -_shotOrigin.forward * _shotRange);
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

            if (Physics.Raycast(_shotOrigin.position, -_shotOrigin.forward, 
                               out RaycastHit hit, _shotRange, _shotLayerMask))
            {
                if (hit.collider.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy.Stagger();
                }
            }
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