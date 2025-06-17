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
    [SerializeField] private HotbarInventory _weaponInventory;
    

    // Values
    private Vector2 _lookInput;
    private Vector2 _currentMouseDelta;
    private Vector2 _currentMouseVelocity;
    [SerializeField] private Transform lookTarget;
    private float _lookTargetRotX;

    private Vector2 _moveInput;
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _moveVelocity = Vector3.zero;

    private bool _isSliding;

    private bool _isCrouching;

    private bool _isJumping;
    private bool _canJump;
    private float _jumpVelocity;
    private float _groundTimer;
    private float _currentJumpHoldTime;

    [SerializeField] private Transform playerHands;


    // Other
    [SerializeField] private PlayerMovementConfig movementConfig;
    [SerializeField] private GroundCheck groundCheck;

    [SerializeField] private FirearmConfig firearmConfig;



    // Functions
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();

        _inputController = GetComponent<InputController>();
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
            
            _inputController.EquipEvent += HandleEquipInput;

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

            _inputController.EquipEvent -= HandleEquipInput;
            
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

    private void HandleEquipInput(int index)
    {
        EquipWeapon(index);
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


        Vector3 targetVelocity = Vector3.zero;

        Move(ref targetVelocity); // Handles Slide + Crouch
        Jump(ref targetVelocity);

        ApplyAccelDecelRates(ref targetVelocity);


        _moveVelocity = targetVelocity;

        _controller.Move(_moveVelocity * Time.deltaTime);
    }

    private void Look()
    {
        Vector2 targetDelta = _lookInput * movementConfig.lookSpeed;

        _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetDelta,
            ref _currentMouseVelocity, movementConfig.lookSmoothTime);

        // Left/Right
        transform.Rotate(Vector3.up, _currentMouseDelta.x);

        // Up/Down
        _lookTargetRotX = HelpfulFunctions.Clamp(_lookTargetRotX - _currentMouseDelta.y, -movementConfig.xCameraBounds, movementConfig.xCameraBounds);

        lookTarget.localRotation = Quaternion.AngleAxis(_lookTargetRotX, Vector3.right);
    }

    private void Move(ref Vector3 velocity)
    {
        _moveDirection = transform.forward * _moveInput.y + transform.right * _moveInput.x;
        _moveDirection.Normalize();

        velocity = _moveDirection * movementConfig.targetMoveSpeed;
    }

    private void Jump(ref Vector3 velocity)
    {
        if (groundCheck.IsGrounded)
        {
            // Cooldown to ensure that you can still Jump when walking down Ramps (might not always be Grounded)
            _groundTimer = movementConfig.groundedTimer;
            _jumpVelocity = 0.0f;
        }

        if (_groundTimer > 0.0f)
        {
            _groundTimer -= Time.deltaTime;
            _canJump = true;
        }


        // Always apply Gravity in case of Jumps/Ramps/Ledges/Falls/Etc
        if (_jumpVelocity < 0.0f)
            _jumpVelocity -= movementConfig.gravityMultiplier * 2.0f * Time.deltaTime;
        else
            _jumpVelocity -= movementConfig.gravityMultiplier * Time.deltaTime;


        // Actual Jump
        if (_isJumping && _canJump)
        {
            // Can Jump as long as Player was recently Grounded
            if (_groundTimer > 0.0f)
            {
                _groundTimer = 0.0f;
                _jumpVelocity += movementConfig.initialJumpForce;
                
                _currentJumpHoldTime -= Time.deltaTime;
            }

            // Handle Jump Hold
            if (_groundTimer == 0.0f && _currentJumpHoldTime < movementConfig.maxJumpHoldTime)
            {
                _jumpVelocity += movementConfig.holdtimeJumpForce;
                _currentJumpHoldTime -= Time.deltaTime;
            }

            if (_currentJumpHoldTime < 0.0f)
                _canJump = false;
        }

        if (!_isJumping || !_canJump)
        {
            _currentJumpHoldTime = movementConfig.maxJumpHoldTime;
        }


        AdjustVelocityToSlope(ref velocity);

        velocity.y += _jumpVelocity;
    }

    private void AdjustVelocityToSlope(ref Vector3 velocity)
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, movementConfig.slopeCheckDistance))
        {
            Quaternion slopeRot = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

            Vector3 adjustedVelocity = slopeRot * velocity;

            if (adjustedVelocity.y < 0.0f)
            {
                velocity = adjustedVelocity;
            }
        }
    }

    private void ApplyAccelDecelRates(ref Vector3 velocity)
    {
        float accel = _moveInput != Vector2.zero ? movementConfig.accelerationRate : movementConfig.decelerationRate;

        float acceleration = groundCheck.IsGrounded ? accel : accel * movementConfig.airControlFactor;
        velocity = Vector3.MoveTowards(_moveVelocity, velocity, acceleration * Time.deltaTime);
    }


    // DESTROY EQUIPPED ITEM METHOD
    private void EquipWeapon(int weaponIndex)
    {
        if (equippedWeapon != null)
        {
            Destroy(equippedWeapon);
        }

        Weapon weaponToEquip = _weaponInventory.ReturnItem(weaponIndex);
        
        equippedWeapon = Instantiate(weaponToEquip, playerHands);
    }
    
    // DE-ACTIVATE EQUIPPED ITEM METHOD
    
}
