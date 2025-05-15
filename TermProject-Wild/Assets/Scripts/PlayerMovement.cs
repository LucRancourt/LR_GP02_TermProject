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
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _moveVelocity = Vector3.zero;

    private bool _isJumping = false;
    private float _jumpVelocity = 0.0f;
    private float _groundTimer = 0.0f;
    private float _currentJumpHoldTime = 0.0f;

    private Vector2 _lookInput;
    [SerializeField] private GameObject _camera;
    private Vector2 _currentMouseDelta;
    private float _xRotation;
    private Vector2 _currentMouseVelocity;


    // Other
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

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable()
    {
        if (_inputController != null)
        {
            _inputController.MoveEvent += HandleMoveInput;
            _inputController.JumpEvent += HandleJumpInput;
            _inputController.LookEvent += HandleLookInput;
        }
    }

    void OnDisable()
    {
        if (_inputController != null)
        {
            _inputController.MoveEvent -= HandleMoveInput;
            _inputController.JumpEvent -= HandleJumpInput;
            _inputController.LookEvent -= HandleLookInput;
        }
    }

    private void HandleMoveInput(Vector2 movement)
    {
        _moveInput = movement;
    }

    private void HandleJumpInput()
    {
        if (!_isJumping && _groundCheck.IsGrounded)
            _isJumping = true;
    }

    private void HandleLookInput(Vector2 look)
    {
        _lookInput = look;
    }


    
    void Update()
    {
        Look();
        Move();
        Jump();

        _controller.Move(_moveVelocity * Time.deltaTime);
    }

    private void Look()
    {
        Vector2 targetDelta = _lookInput / _movementConfig.lookSpeedDivider;

        _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetDelta,
            ref _currentMouseVelocity, _movementConfig.lookSmoothTime);

        _xRotation -= _currentMouseDelta.y;
        _xRotation = Mathf.Clamp(_xRotation, -_movementConfig.xCameraBounds, _movementConfig.xCameraBounds);

        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);

        transform.Rotate(Vector3.up, _currentMouseDelta.x);
    }

    private void Move()
    {
        _moveDirection = transform.forward * _moveInput.y + transform.right * _moveInput.x;
        _moveDirection.Normalize();

        Vector3 targetVelocity = _moveDirection * _movementConfig.targetMoveSpeed;

        float acceleration = _groundCheck.IsGrounded ? _movementConfig.accelerationRate : _movementConfig.airControlFactor;

        if (_moveInput == Vector2.zero)
            acceleration = _movementConfig.decelerationRate;

        _moveVelocity.x = Mathf.MoveTowards(_moveVelocity.x, targetVelocity.x, acceleration * Time.deltaTime);
        _moveVelocity.z = Mathf.MoveTowards(_moveVelocity.z, targetVelocity.z, acceleration * Time.deltaTime);

        //_moveVelocity = _moveDirection * _movementConfig.targetMoveSpeed;
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
        if (_jumpVelocity > 0.0f)
            _jumpVelocity -= _movementConfig.gravityMultiplier / 2.0f * Time.deltaTime;
        else
            _jumpVelocity -= _movementConfig.gravityMultiplier * Time.deltaTime;


        // Actual Jump
        if (_isJumping)
        {
            // Can Jump as long as Player was recently Grounded
            if (_groundTimer > 0.0f)
            {
                _groundTimer = 0.0f;
                _jumpVelocity += Mathf.Sqrt(_movementConfig.baseJumpForce * 2 * _movementConfig.gravityMultiplier);
                _isJumping = false;
            }
        }


        _moveVelocity.y = _jumpVelocity;
    }
}
