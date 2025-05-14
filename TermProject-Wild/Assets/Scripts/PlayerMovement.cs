using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InputController))]
public class PlayerMovement : MonoBehaviour
{
    // Variables
        // Controllers
    private CharacterController _controller;
    private InputController _inputController;
    
        // Values
    private Vector2 _moveInput;
    private Vector3 _moveVector = Vector3.zero;
    
    private bool _isJumping = false;
    private float _jumpVelocity = 0.0f;
    private float _groundTimer = 0.0f;

    
        // Else
    [SerializeField] private PlayerMovementConfig _movementConfig;
    [SerializeField] private GroundCheck _groundCheck;

    
    
    // Functions
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        
        if (_controller == null)
            Debug.LogError("CharacterController component not found.");
        
        
        _inputController = GetComponent<InputController>();

        if (_inputController == null)
            Debug.LogError("InputController component not found.");
    }

    void OnEnable()
    {
        if (_inputController != null)
        {
            _inputController.MoveEvent += HandleMoveInput;
            _inputController.JumpEvent += HandleJumpInput;
        }
    }

    void OnDisable()
    {
        if (_inputController != null)
        {
            _inputController.MoveEvent -= HandleMoveInput;
            _inputController.JumpEvent -= HandleJumpInput;
        }
    }

    private void HandleMoveInput(Vector2 movement)
    {
        _moveInput = movement;
    }

    private void HandleJumpInput()
    {
        Debug.Log(_groundCheck.IsGrounded);
        if (!_isJumping && _groundCheck.IsGrounded)
        {
            _isJumping = true;
            Debug.Log("eeeeee");
        }
            //_isJumping = true;
    }
    
    void Update()
    {
        Move();
        Jump();

        _controller.Move(_moveVector * Time.deltaTime);
        /*
         * Determine Target Velocity: Based on the _currentMoveInput and the movementConfig.targetMoveSpeed, calculate the desired velocity vector for this frame. Remember to convert the 2D input to a 3D world space vector (considering camera orientation if necessary, though we will keep it world aligned conceptually for now).
// Conceptual
// Vector3 targetDirection = new Vector3(_currentMoveInput.x, 0, _currentMoveInput.y);
// Vector3 targetVelocity = targetDirection * movementConfig.targetMoveSpeed;
Calculate Acceleration/Deceleration: Compare the targetVelocity with the character current velocity. Use the accelerationRate or decelerationRate from the movementConfig (potentially influenced by the IsGrounded state – e.g., less acceleration/control in the air) to smoothly interpolate the current velocity towards the target velocity over time. Functions like Mathf.MoveTowards or Vector3.Lerp (used carefully) are common here. The rate determines how quickly the character speeds up or slows down.
// Conceptual - applying acceleration/deceleration to currentVelocity
// float accel = IsGrounded ? movementConfig.accelerationRate : movementConfig.airAccelerationRate; // Example
// currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, accel * Time.deltaTime);
Handle Gravity: Ensure gravity is applied correctly, especially when airborne. If using CharacterController, gravity needs to be manually added to the vertical component of the velocity each frame. If using Rigidbody, the physics engine handles gravity, but you might adjust its effect using gravityMultiplier from the movementConfig or by managing vertical velocity separately.
// Conceptual - applying gravity if using CharacterController
// if (!IsGrounded) { currentVelocity.y += Physics.gravity.y * movementConfig.gravityMultiplier * Time.deltaTime; }
Apply Final Velocity: Use the calculated currentVelocity (incorporating input, acceleration/deceleration, and gravity) to move the character using either characterController.Move(currentVelocity * Time.deltaTime) or by setting rigidbody.velocity (often done in FixedUpdate for Rigidbody).
         */
    }

    private void Move()
    {
        _moveVector.x = _moveInput.x * _movementConfig.targetMoveSpeed;
        _moveVector.z = _moveInput.y * _movementConfig.targetMoveSpeed;
    }
    
    private void Jump()
    {
        // Credits to: Kurt-Dekker
        // https://discussions.unity.com/t/how-to-correctly-setup-3d-character-movement-in-unity/811250 - first response post

        if (_groundCheck.IsGrounded)
            // Cooldown to ensure that you can still Jump when walking down Ramps (might not always be Grounded)
            _groundTimer = _movementConfig.groundedTimer;

        if (_groundTimer > 0.0f)
            _groundTimer -= Time.deltaTime;


        
        if (_groundCheck.IsGrounded && _jumpVelocity < 0)
            // Hit the Ground
            _jumpVelocity = 0f;


        // Always apply Gravity in case of Ramps/Ledges/Falls/Etc
        _jumpVelocity -= _movementConfig.gravityMultiplier * Time.deltaTime;


        // Actual Jump
        if (_isJumping)
        {
            // Can Jump as long Player was recently Grounded
            if (_groundTimer > 0.0f)
            {
                _groundTimer = 0.0f;
                _jumpVelocity += Mathf.Sqrt(_movementConfig.baseJumpForce * 2 * _movementConfig.gravityMultiplier);
                _isJumping = false;
            }
        }


        _moveVector.y = _jumpVelocity;
    }
}
