using Features.Actor.Models;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private float speedRotation = 5f;
    private float _currentSpeedRotation;

    public float SpeedRotation { get => speedRotation; set => speedRotation = value; }

    private float runSpeed = 5;
    private float walkSpeed = 2;
    private bool isRunning;
    
    public Animator _animator;
    private Vector3 movement;
    private Rigidbody _rigidbody;



    void Start()
    {
        _animator = GetComponent<Animator>();

        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movement.z = Input.GetAxisRaw("Horizontal");
        movement.x = -Input.GetAxisRaw("Vertical");

        isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (movement == Vector3.zero)
        {
            _animator.SetInteger("MovementState", (int)MovementState.Idle);
            return;
        }
        _animator.SetInteger("MovementState", (int) (isRunning ? MovementState.Run : MovementState.Walk));
    }

    private void FixedUpdate()
    {
        if (movement == Vector3.zero) 
            return;
        
        var speed = isRunning ? runSpeed : walkSpeed;
        var moveDirection = movement.normalized;
            
        _rigidbody.MovePosition(_rigidbody.position + moveDirection * speed * Time.deltaTime);
            
        var lookRotation = moveDirection != Vector3.zero ? Quaternion.LookRotation(moveDirection) : transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * SpeedRotation);
    }
}


