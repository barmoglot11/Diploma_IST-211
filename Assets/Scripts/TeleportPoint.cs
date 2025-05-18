using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using BATTLE;

[RequireComponent(typeof(Collider))] // Требует компонент Collider
public class TeleportPoint : MonoBehaviour
{
    [Header("Настройки телепорта")]
    [Tooltip("Точка назначения для телепортации")]
    [SerializeField] private Transform _destinationPoint;
    [Tooltip("Требуется ли нажатие кнопки для активации")]
    [SerializeField] private bool _requireInput = true;
    [Tooltip("Клавиша для активации телепорта")]
    [SerializeField] private KeyCode _activationKey = KeyCode.E;
    [Tooltip("Время перезарядки телепорта")]
    [SerializeField] private float _cooldown = 2f;
    
    [Header("События")]
    [Tooltip("Событие при начале телепортации")]
    [SerializeField] private UnityEvent _onTeleportStart;
    [Tooltip("Событие после завершения телепортации")]
    [SerializeField] private UnityEvent _onTeleportEnd;

    private bool _isPlayerInRange; // Игрок в зоне действия
    private bool _isOnCooldown;   // Телепорт на перезарядке
    private MainCharacter _player; // Ссылка на игрока

    private void Update()
    {
        //StartTeleport();
    }

    public void StartTeleport()
    {
        // Проверяем условия для телепортации
        if (_isPlayerInRange && !_isOnCooldown)
        {
            // Если не требуется ввод или нажата нужная клавиша
            if (!_requireInput || Input.GetKeyDown(_activationKey))
            {
                StartCoroutine(TeleportRoutine());
            }
        }
    }

    public void ClearTeleportEvents()
    {
        _onTeleportStart.RemoveAllListeners();
        _onTeleportEnd.RemoveAllListeners();
    }
    
    // Процесс телепортации
    private IEnumerator TeleportRoutine()
    {
        _isOnCooldown = true;
        _onTeleportStart?.Invoke(); // Запускаем событие
        
        // Небольшая задержка для эффектов
        yield return new WaitForSeconds(0.1f);
        
        // Телепортируем игрока, если все условия выполнены
        if (_player != null && _destinationPoint != null)
        {
            _player.transform.position = _destinationPoint.position;
            _player.transform.rotation = _destinationPoint.rotation;
        }
        yield return new WaitForSeconds(_cooldown);
        _onTeleportEnd?.Invoke(); // Событие завершения
        _isOnCooldown = false; // Сбрасываем перезарядку
    }

    // Игрок вошел в триггер
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<MainCharacter>(out var player))
        {
            _isPlayerInRange = true;
            _player = player;
        }
    }

    // Игрок вышел из триггера
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<MainCharacter>(out _))
        {
            _isPlayerInRange = false;
            _player = null;
        }
    }

    // Проверки в редакторе
    private void OnValidate()
    {
        if (_destinationPoint == null)
        {
            Debug.LogWarning($"Не назначена точка назначения на {name}", this);
        }

        // Автоматически делаем коллайдер триггером
        var collider = GetComponent<Collider>();
        if (collider != null && !collider.isTrigger)
        {
            collider.isTrigger = true;
        }
    }
}