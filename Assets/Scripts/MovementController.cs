using Features.Actor.Models;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private float _currentSpeedRotation;
    private float SpeedRotation { get; set; } = 5f;

    private const float RunSpeed = 5;
    private const float WalkSpeed = 2;
    private bool _isRunning;
    
    private Animator _animator;
    private Vector3 _movement;
    private Rigidbody _rigidbody;
    
    private static readonly int MovementStateHash = Animator.StringToHash("MovementState");


    private void Start()
    {
        _animator = GetComponent<Animator>();

        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _movement.z = Input.GetAxisRaw("Horizontal");
        _movement.x = -Input.GetAxisRaw("Vertical");

        _isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (_movement == Vector3.zero)
        {
            _animator.SetInteger(MovementStateHash, (int)MovementState.Idle);
            return;
        }
        _animator.SetInteger(MovementStateHash, (int) (_isRunning ? MovementState.Run : MovementState.Walk));
    }

    private void FixedUpdate()
    {
        if (_movement == Vector3.zero) 
            return;
        
        var speed = _isRunning ? RunSpeed : WalkSpeed;
        var moveDirection = _movement.normalized;
            
        _rigidbody.MovePosition(_rigidbody.position + moveDirection * speed * Time.deltaTime);
            
        var lookRotation = moveDirection != Vector3.zero ? Quaternion.LookRotation(moveDirection) : transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * SpeedRotation);
    }
}


