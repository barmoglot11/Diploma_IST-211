using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(Collider))]
[DisallowMultipleComponent]
public class InteractiveObject : MonoBehaviour, IInteractable
{
    [Header("Interaction Settings")] [SerializeField]
    private bool _isInteractable = true;

    [SerializeField] private bool _singleUse = false;
    [SerializeField] private float _interactionCooldown = 0f;

    [Header("Events")] [FormerlySerializedAs("OnInteract")] [SerializeField]
    private UnityEvent _onInteract;

    [SerializeField] private UnityEvent _onInteractionEnabled;
    [SerializeField] private UnityEvent _onInteractionDisabled;

    [Header("Debug")] [SerializeField] private bool _debugLogs = true;

    private float _lastInteractionTime;
    private bool _hasBeenUsed;
    private Collider _collider;


    public bool IsInteractable
    {
        get => _isInteractable;
        set
        {
            if (_isInteractable != value)
            {
                _isInteractable = value;
                if (value) _onInteractionEnabled?.Invoke();
                else _onInteractionDisabled?.Invoke();

                if (_debugLogs) Debug.Log($"{name} interactability set to {value}");
            }
        }
    }

    private bool IsCooldownEnabled =>
        !CanInteract()
        && !_singleUse
        && !(_interactionCooldown <= 0);

    private void Update()
    {
        if (_lastInteractionTime + _interactionCooldown < Time.time
            && IsCooldownEnabled)
        {
            EnableInteraction();
        }
    }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        if (_collider != null && !_collider.isTrigger)
        {
            _collider.isTrigger = true;
            Debug.LogWarning($"Collider on {name} was not set as trigger. Fixed automatically.", this);
        }
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        _lastInteractionTime = Time.time;
        _hasBeenUsed = true;

        try
        {
            _onInteract?.Invoke();
            if (_debugLogs) Debug.Log($"Interacted with {name}");

            if (_singleUse || !(_interactionCooldown <= 0f))
            {
                DisableInteraction();
                if (_debugLogs) Debug.Log($"{name} disabled after single use");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Interaction error on {name}: {e.Message}", this);
        }
    }

    private bool CanInteract()
    {
        if (!_isInteractable)
        {
            if (_debugLogs) Debug.Log($"{name} is currently not interactable");
            return false;
        }

        if (_hasBeenUsed && _singleUse)
        {
            if (_debugLogs) Debug.Log($"{name} has already been used and is single-use");
            return false;
        }

        if (Time.time < _lastInteractionTime + _interactionCooldown)
        {
            if (_debugLogs) Debug.Log($"{name} is on cooldown");
            return false;
        }

        return true;
    }

    public void EnableInteraction()
    {
        IsInteractable = true;
        Debug.Log($"{name} has been enabled");
    }

    public void DisableInteraction()
    {
        IsInteractable = false;
        Debug.Log($"{name} has been disabled");
    }

    public void ResetUsage()
    {
        _hasBeenUsed = false;
        if (_debugLogs) Debug.Log($"{name} usage reset");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !_isInteractable) return;

        if (InteractionManager.Instance == null) return;
        InteractionManager.Instance.AddInteract(Interact);
        if (_debugLogs) Debug.Log($"Registered interaction for {name}");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        UnregisterInteraction();
    }

    private void OnDisable() => UnregisterInteraction();
    private void OnDestroy() => UnregisterInteraction();

    private void UnregisterInteraction()
    {
        if (InteractionManager.Instance != null)
        {
            InteractionManager.Instance.DeleteInteract(Interact);
            if (_debugLogs) Debug.Log($"Unregistered interaction for {name}");
        }
    }

    // Editor helper
    private void OnValidate()
    {
        if (_interactionCooldown < 0)
            _interactionCooldown = 0;
    }
}