using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    CharacterController controller;
    
    [SerializeField]
    InputReader input;
    
    private Vector2 _moveDirection;

    [SerializeField]
    private Transform cam;
    
    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private void Start()
    {
        input.MoveEvent += HandleMove;
    }

    private void Update()
    {
        Move();
    }

    private void HandleMove(Vector2 direction)
    {
        _moveDirection = direction;
    }

    private void Move()
    {
        Vector3 direction = new Vector3(_moveDirection.x, 0f, _moveDirection.y).normalized;

        if(direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg+ cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * (speed * Time.deltaTime));
        }
    }
}
