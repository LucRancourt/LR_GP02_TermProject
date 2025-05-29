using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InputController))]
public class PlayerMovement : MonoBehaviour
{
    // Variables
    // Controllers
    private CharacterController _controller;
    private InputController _inputController;

    // Weapon
    [SerializeField] private Weapon equippedWeapon;

    // Values
    private Vector2 _lookInput;
    private Vector2 _currentMouseDelta;
    private Vector2 _currentMouseVelocity;

    private Vector2 _moveInput;
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _moveVelocity = Vector3.zero;

    private bool _isSliding;

    private bool _isCrouching;

    private bool _isJumping;
    private float _jumpVelocity;
    private float _groundTimer;
    private float _currentJumpHoldTime;


    // Other
    [SerializeField] private PlayerMovementConfig movementConfig;
    [SerializeField] private GroundCheck groundCheck;

    [SerializeField] private FirearmConfig firearmConfig;



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

    private void OnEnable()
    {
        if (_inputController != null)
        {
            _inputController.LookEvent += HandleLookInput;

            _inputController.MoveEvent += HandleMoveInput;

            _inputController.SlideEvent += HandleSlideInput;

            _inputController.CrouchEvent += HandleCrouchInput;
            _inputController.CrouchCancelEvent += HandleCrouchCancelInput;

            _inputController.JumpEvent += HandleJumpInput;
            _inputController.JumpCancelEvent += HandleJumpCancelInput;

            _inputController.FireEvent += HandleFireInput;
            _inputController.FireCancelEvent += HandleFireCancelInput;

            _inputController.ReloadEvent += HandleReloadInput;
        }
    }

    private void OnDisable()
    {
        if (_inputController != null)
        {
            _inputController.LookEvent -= HandleLookInput;

            _inputController.MoveEvent -= HandleMoveInput;

            _inputController.SlideEvent -= HandleSlideInput;

            _inputController.CrouchEvent -= HandleCrouchInput;
            _inputController.CrouchCancelEvent -= HandleCrouchCancelInput;

            _inputController.JumpEvent -= HandleJumpInput;
            _inputController.JumpCancelEvent -= HandleJumpCancelInput;

            _inputController.FireEvent -= HandleFireInput;
            _inputController.FireCancelEvent -= HandleFireCancelInput;

            _inputController.ReloadEvent += HandleReloadInput;
        }
    }

    private void HandleLookInput(Vector2 look)
    {
        _lookInput = look;
    }

    private void HandleMoveInput(Vector2 movement)
    {
        _moveInput = movement;
    }

    private void HandleSlideInput()
    {
        _isSliding = true;
    }

    private void HandleCrouchInput()
    {
        _isCrouching = true;
    }

    private void HandleCrouchCancelInput()
    {
        _isCrouching = false;
    }

    private void HandleJumpInput()
    {
        _isJumping = true;
    }

    private void HandleJumpCancelInput()
    {
        _isJumping = false;
    }

    private void HandleFireInput()
    {
        equippedWeapon.Use();
    }

    private void HandleFireCancelInput()
    {
        equippedWeapon.StopUsing();
    }

    private void HandleReloadInput()
    {
        //equippedWeapon.Reload(30);
    }



    void Update()
    {
        Look();
        Move(); // Handles Slide + Crouch
        Jump();

        _controller.Move(_moveVelocity * Time.deltaTime);
    }

    private void Look()
    {
        Vector2 targetDelta = _lookInput / movementConfig.lookSpeedDivider;

        _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetDelta,
            ref _currentMouseVelocity, movementConfig.lookSmoothTime);

        transform.Rotate(Vector3.up, _currentMouseDelta.x);
    }

    private void Move()
    {
        _moveDirection = transform.forward * _moveInput.y + transform.right * _moveInput.x;
        _moveDirection.Normalize();

        Vector3 targetVelocity = _moveDirection * movementConfig.targetMoveSpeed;

        float accel = _moveInput != Vector2.zero ? movementConfig.accelerationRate : movementConfig.decelerationRate;
        float acceleration = groundCheck.IsGrounded ? accel : accel * movementConfig.airControlFactor;

        _moveVelocity = Vector3.MoveTowards(_moveVelocity, targetVelocity, acceleration * Time.deltaTime);
    }

    private void Jump()
    {
        // Credits to: Kurt-Dekker
        // https://discussions.unity.com/t/how-to-correctly-setup-3d-character-movement-in-unity/811250 - first response post

        if (groundCheck.IsGrounded)
            // Cooldown to ensure that you can still Jump when walking down Ramps (might not always be Grounded)
            _groundTimer = movementConfig.groundedTimer;

        if (_groundTimer > 0.0f)
            _groundTimer -= Time.deltaTime;



        if (groundCheck.IsGrounded && _jumpVelocity < 0)
            // Hit the Ground
            _jumpVelocity = 0f;


        // Always apply Gravity in case of Ramps/Ledges/Falls/Etc
        if (_jumpVelocity > 0.0f)
            _jumpVelocity -= movementConfig.gravityMultiplier / 2.0f * Time.deltaTime;
        else
            _jumpVelocity -= movementConfig.gravityMultiplier * Time.deltaTime;


        // Actual Jump
        if (_isJumping)
        {
            // Can Jump as long as Player was recently Grounded
            if (_groundTimer > 0.0f)
            {
                _groundTimer = 0.0f;
                _jumpVelocity += Mathf.Sqrt(movementConfig.baseJumpForce * 2 * movementConfig.gravityMultiplier);

                _currentJumpHoldTime -= Time.deltaTime;
            }

            // Handle Jump Hold
            if (_groundTimer == 0.0f && _currentJumpHoldTime < movementConfig.maxJumpHoldTime)
            {
                _jumpVelocity += Mathf.Sqrt(movementConfig.baseJumpForce);
                _currentJumpHoldTime -= Time.deltaTime;
            }

            if (_currentJumpHoldTime < 0.0f)
                _isJumping = false;
        }

        if (!_isJumping)
        {
            _currentJumpHoldTime = movementConfig.maxJumpHoldTime;
        }


        _moveVelocity.y = _jumpVelocity;
    }
}
