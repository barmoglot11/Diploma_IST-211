using System;
using MANAGER;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb => GetComponent<Rigidbody>();
    
    AnimationManager animManager => AnimationManager.Instance;
    InputReader input => InputManager.Instance.IR;
    
    private Vector2 _moveDirection;

    [SerializeField]
    private Transform cam;
    
    public float speed = 5f;
    public int speedMult = 3;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public bool Hurry = false;
    private void Start()
    {
        SetupInput();
    }

    void SetupInput()
    {
        input.MoveEvent += HandleMove;
        input.HurryEvent += HandleHurry;
        input.SnipeEvent += Snipe;
    }
    
    private void OnEnable()
    {
        SetupInput();
    }

    private void OnDisable()
    {
        input.MoveEvent -= HandleMove;
        input.HurryEvent -= HandleHurry;
        input.SnipeEvent -= Snipe;
    }

    private void Update()
    {
        Move();
    }

    public void Snipe(bool state) => animManager.IsSniping = state;
    
    private void HandleMove(Vector2 direction)
    {
        _moveDirection = direction;
    }
    private void HandleHurry(bool state)
    {
        Hurry = state;
        animManager.IsRunning = state;
    }
    private void Move()
    {
        Vector3 direction = new Vector3(_moveDirection.x, 0f, _moveDirection.y).normalized;

        if (direction.magnitude > 0.1f)
        {
            var SpeedFinal = speed;
            if (Hurry)
                SpeedFinal *= speedMult;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.velocity = moveDir.normalized * SpeedFinal;
            animManager.IsWalking = true;
        }
        else
        {
            animManager.IsWalking = false;
        }
    }
}
