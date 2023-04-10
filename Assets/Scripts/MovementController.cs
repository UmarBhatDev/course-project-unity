using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private bool aimStatus;
    private float speedRotation = 5f;
    
    private float rangeForRun = 350f;
    private float _currentSpeedRotation;

    public float WalkSpeed { get => walkSpeed; set => walkSpeed = value; }
    public float SpeedRotation { get => speedRotation; set => speedRotation = value; }
    public float RunSpeed { get => runSpeed; set => runSpeed = value; }
    public float CurrentSpeedRotation { get => _currentSpeedRotation; set => _currentSpeedRotation = value; }

    // private void Start()
    // {
    //     CurrentSpeedRotation = SpeedRotation;
    //     _rigidbody = GetComponent<Rigidbody>();
    //     _animator = GetComponent<Animator>();
    // }
    //
    // // private void FixedUpdate()
    // {
    //     Move();
    // }

    // private void Move()
    // {
    //     var inputDirection = new Vector2();
    //     
    //     if (Input.GetKeyDown(KeyCode.W))
    //     {
    //         inputDirection.y = 1;
    //     }
    //     if (Input.GetKeyDown(KeyCode.S))
    //     {
    //         inputDirection.y = -1;
    //     }
    //     if (Input.GetKeyDown(KeyCode.A))
    //     {
    //         inputDirection.x = 1;
    //     }
    //     if (Input.GetKeyDown(KeyCode.D))
    //     {
    //         inputDirection.y = -1;
    //     }
    //
    //     Vector2 direction = inputDirection;
    //     Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
    //
    //     Quaternion lookRotation =
    //         moveDirection != Vector3.zero ? Quaternion.LookRotation(moveDirection) : transform.rotation;
    //
    //     transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * CurrentSpeedRotation);
    //
    //     float speed = RunSpeed;
    //
    //     _animator.SetInteger("MoveState", (int)speed);
    //
    //     _rigidbody.AddForce(moveDirection * speed);
    // }
    
    
    private float runSpeed = 5;
    private float walkSpeed = 2;
    public Animator _animator;
    private Vector3 movement;
    private bool isRunning;
    private Rigidbody _rigidbody;


    void Start()
    {
        CurrentSpeedRotation = SpeedRotation;
        _animator = GetComponent<Animator>();

        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movement.z = Input.GetAxisRaw("Horizontal");
        movement.x = -Input.GetAxisRaw("Vertical");

        // Check if running
        isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (movement != Vector3.zero)
        {
            _animator.SetInteger("MovementState", (int) (isRunning ? MovementState.Run : MovementState.Walk));
        }
        else
        {
            _animator.SetInteger("MovementState", (int)MovementState.Idle);
        }
    }

    void FixedUpdate()
    {
        // Move the player
        if (movement != Vector3.zero)
        {
            float speed = isRunning ? runSpeed : walkSpeed;
            var moveDirection = movement.normalized;
            
            _rigidbody.MovePosition(_rigidbody.position + moveDirection * speed * Time.deltaTime);
            
            var lookRotation = moveDirection != Vector3.zero ? Quaternion.LookRotation(moveDirection) : transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * CurrentSpeedRotation);
            
        }
    }
}

public enum MovementState
{
    Idle = 0,
    Walk = 1,
    Run = 2,
}

