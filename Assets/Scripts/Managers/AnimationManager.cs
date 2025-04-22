using System;
using UnityEngine;

namespace MANAGER
{
    /// <summary>
    /// Централизованный менеджер анимаций с поддержкой событий и расширенными параметрами
    /// </summary>
    public class AnimationManager : MonoBehaviour
    {
        public static AnimationManager Instance { get; private set; }

        [Header("Основные компоненты")]
        [SerializeField] private Animator _animator;
        [Tooltip("Автоматически обновлять параметры аниматора каждый кадр")]
        [SerializeField] private bool _autoUpdate = true;

        [Header("Состояния анимаций")]
        [SerializeField] private bool _isWalking;
        [SerializeField] private bool _isRunning;
        [SerializeField] private bool _isSniping;

        // События для отслеживания изменений состояний
        public event Action<bool> OnWalkingStateChanged;
        public event Action<bool> OnRunningStateChanged;
        public event Action<bool> OnSnipingStateChanged;

        #region Свойства с уведомлениями
        public bool IsWalking
        {
            get => _isWalking;
            set
            {
                if (_isWalking != value)
                {
                    _isWalking = value;
                    OnWalkingStateChanged?.Invoke(_isWalking);
                    if (_autoUpdate) UpdateAnimator();
                }
            }
        }

        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    OnRunningStateChanged?.Invoke(_isRunning);
                    if (_autoUpdate) UpdateAnimator();
                }
            }
        }

        public bool IsSniping
        {
            get => _isSniping;
            set
            {
                if (_isSniping != value)
                {
                    _isSniping = value;
                    OnSnipingStateChanged?.Invoke(_isSniping);
                    if (_autoUpdate) UpdateAnimator();
                }
            }
        }
        #endregion

        private void Awake()
        {
            InitializeSingleton();
            ValidateComponents();
        }

        private void Update()
        {
            if (_autoUpdate)
            {
                UpdateAnimator();
            }
        }

        #region Основные методы
        private void InitializeSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
                //DontDestroyOnLoad(gameObject); // Раскомментировать если нужно сохранять между сценами
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void ValidateComponents()
        {
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
                if (_animator == null)
                {
                    Debug.LogError("Animator component is missing!", this);
                }
            }
        }

        /// <summary>
        /// Принудительно обновить все параметры аниматора
        /// </summary>
        public void UpdateAnimator()
        {
            if (_animator == null) return;

            _animator.SetBool("IsWalking", _isWalking);
            _animator.SetBool("IsRunning", _isRunning);
            _animator.SetBool("IsSniping", _isSniping);
        }
        #endregion

        #region Дополнительные функции
        /// <summary>
        /// Сбросить все состояния анимаций
        /// </summary>
        public void ResetAllStates()
        {
            IsWalking = false;
            IsRunning = false;
            IsSniping = false;
        }

        /// <summary>
        /// Установить несколько состояний одновременно
        /// </summary>
        public void SetStates(bool walking, bool running, bool sniping)
        {
            IsWalking = walking;
            IsRunning = running;
            IsSniping = sniping;
        }
        #endregion
    }
}
