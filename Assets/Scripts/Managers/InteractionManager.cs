using System;
using UnityEngine;

/// <summary>
/// Менеджер управления взаимодействиями с системой событий и защитой от ошибок
/// </summary>
public class InteractionManager : MonoBehaviour
{
    #region Singleton Pattern
    public static InteractionManager Instance { get; private set; }

    private void Awake()
    {
        InitializeSingleton();
    }

    private void InitializeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // Раскомментировать для сохранения между сценами
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Event Management
    /// <summary>
    /// Добавить обработчик взаимодействия с проверкой на null
    /// </summary>
    public void AddInteract(Action action)
    {
        if (action == null)
        {
            Debug.LogWarning("Attempt to add null interact action", this);
            return;
        }

        if (InputManager.Instance?.IR != null)
        {
            InputManager.Instance.IR.InteractEvent += action;
        }
        else
        {
            Debug.LogError("InputReader is not available!", this);
        }
    }

    /// <summary>
    /// Удалить обработчик взаимодействия с проверкой на null
    /// </summary>
    public void DeleteInteract(Action action)
    {
        if (action == null)
        {
            Debug.LogWarning("Attempt to remove null interact action", this);
            return;
        }

        if (InputManager.Instance?.IR != null)
        {
            InputManager.Instance.IR.InteractEvent -= action;
        }
        else
        {
            Debug.LogError("InputReader is not available!", this);
        }
    }

    /// <summary>
    /// Очистить все обработчики взаимодействия
    /// </summary>
    public void ClearAllInteracts()
    {
        if (InputManager.Instance?.IR != null)
        {
            // Получаем все подписчики события
            if (InputManager.Instance.IR.GetInteractSubscribersCount() <= 0) return;
            var subscribers = InputManager.Instance.IR.GetInteractDelegate()?.GetInvocationList();

            if (subscribers == null) return;
            foreach (var subscriber in subscribers)
            {
                InputManager.Instance.IR.InteractEvent -= (Action)subscriber;
            }

        }
    }
    #endregion

    #region Utility Methods
    private void OnDestroy()
    {
        // Автоматическая очистка при уничтожении объекта
        if (Instance == this)
        {
            ClearAllInteracts();
        }
    }
    #endregion
}
