using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InputController))]

[RequireComponent(typeof(Animator))]

[RequireComponent(typeof(HotbarInventory))]


public class PlayerMovement : MonoBehaviour
{
    // Animator
    private Animator _animator;
    private AnimatorStateInfo _previousAnimState;
    
    
    // Controllers
    private CharacterController _controller;
    private InputController _inputController;
    
    
    // Cinemachines
    [SerializeField] private CinemachineStateDrivenCamera cmStateCamDriver;
    [SerializeField] private CinemachineCamera aimCam;


    // Weapon
    private Weapon equippedWeapon;
    private HotbarInventory weaponInventory;


    // Variables
    #region Animator

    [SerializeField] private float maxTimeToIdle = 5.0f;
    private float _timeToIdle = 5.0f;

        // States

    private readonly int _hashMovement = Animator.StringToHash("Movement");
    private readonly int _hashMovementBack = Animator.StringToHash("MovementBack");


        // Parameters

    private readonly int _hashIsGrounded = Animator.StringToHash("IsGrounded");
    private readonly int _hashForwardVelocity = Animator.StringToHash("ForwardVelocity");
    private readonly int _hashIsMovingForward = Animator.StringToHash("IsMovingForward");
    private readonly int _hashVerticalVelocity = Animator.StringToHash("VerticalVelocity");
    private readonly int _hashRandomIdle = Animator.StringToHash("RandomIdle");
    private readonly int _hashInputDetected = Animator.StringToHash("InputDetected");
    private readonly int _hashMeleeAttack = Animator.StringToHash("MeleeAttack");
    private readonly int _hashTimeToIdle = Animator.StringToHash("TimeToIdle");

    #endregion

    #region Look
    private Vector2 _lookInput;
    private Vector2 _currentMouseDelta;
    private Vector2 _currentMouseVelocity;
    [SerializeField] private Transform lookTarget;
    private float _lookTargetRotX;
    #endregion

    #region Move
    private Vector2 _moveInput;
    private Vector2 _previousInput = Vector3.zero;
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _moveVelocity = Vector3.zero;
    private Vector3 _targetVelocity;
    #endregion

    private bool _isSliding;

    private bool _isCrouching;

    #region Jump
    private float _jumpBufferTime;
    private bool _isJumping;
    private float _coyoteTime;
    private bool _canHold;
    private float _jumpVelocity;
    private float _currentJumpHoldTime;
    #endregion

    #region Inputs

    private bool _inputIsAttacking;
    private bool _inputIsJumping;
    private bool _inputIsLooking;
    //private bool _inputIsMoving;

    [SerializeField] private float inputTimeActiveFloat = 0.5f;
    private WaitForSeconds _inputTimeActiveSeconds;

    #endregion

    [SerializeField] private Transform playerHands;


    // Configs
    [SerializeField] private PlayerMovementConfig movementConfig;
    [SerializeField] private GroundCheck groundCheck;

    [SerializeField] private FirearmConfig firearmConfig;



    // Functions
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();

        _inputController = GetComponent<InputController>();


        _animator = GetComponent<Animator>();
        _previousAnimState = _animator.GetCurrentAnimatorStateInfo(0);

        weaponInventory = GetComponent<HotbarInventory>();

        _inputTimeActiveSeconds = new WaitForSeconds(inputTimeActiveFloat);
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    #region InputController Enable/Disable

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

            _inputController.AimEvent += HandleAimInput;
            _inputController.AimCancelEvent += HandleAimCancelInput;

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

            _inputController.AimEvent -= HandleAimInput;
            _inputController.AimCancelEvent -= HandleAimCancelInput;

            _inputController.ReloadEvent -= HandleReloadInput;
        }
    }

    #endregion

    #region HandleInputs

    private void HandleLookInput(Vector2 look)
    {
        _lookInput = look;

        StartCoroutine(SetInputVar(InputSwitchValues.Look));
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
        _jumpBufferTime = movementConfig.jumpBufferTime;

        StartCoroutine(SetInputVar(InputSwitchValues.Jump));
    }

    private void HandleJumpCancelInput()
    {
        _isJumping = false;
    }

    private void HandleEquipInput(float index)
    {
        EquipWeapon((int)index);
    }

    private void HandleFireInput()
    {
        if (equippedWeapon == null) return;

        equippedWeapon.Use();

        StartCoroutine(SetInputVar(InputSwitchValues.Attack));
    }

    private void HandleFireCancelInput()
    {
        if (equippedWeapon == null) return;

        equippedWeapon.StopUsing();
    }

    private void HandleAimInput()
    {
        aimCam.gameObject.SetActive(true);
        cmStateCamDriver.gameObject.SetActive(false);
    }
    private void HandleAimCancelInput()
    {
        cmStateCamDriver.gameObject.SetActive(true);
        aimCam.gameObject.SetActive(false);
    }

    private void HandleReloadInput()
    {
        if (equippedWeapon == null || equippedWeapon is MeleeWeapon) return;

        RangedWeapon ranged = (RangedWeapon)equippedWeapon;
        ranged.Reload(30);
    }


    IEnumerator SetInputVar(InputSwitchValues inputToSet)
    {
        InputSwitch(inputToSet, true);

        yield return _inputTimeActiveSeconds;

        InputSwitch(inputToSet, false);
    }

    private void InputSwitch(InputSwitchValues inputToSwitch, bool value)
    {
        switch ((int)inputToSwitch)
        {
            case 0:
                _inputIsAttacking = value;
                break;

            case 1:
                _inputIsJumping = value;
                break;

            case 2:
                _inputIsLooking = value;
                break;
        }
    }

    #endregion


    private void Update()
    {
        UpdateAnimatorVars();
    }

    private void FixedUpdate()
    {
        Look();

        ApplyGravity();

        Move(); // Handles Slide + Crouch

        Jump();

        AdjustVelocityToSlope();

        ApplyAccelDecelRates();


        _moveVelocity = _targetVelocity;

        _controller.Move(_moveVelocity * Time.fixedDeltaTime);
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

    private void ApplyGravity()
    {
        // Always apply Gravity in case of Jumps/Ramps/Ledges/Falls/Etc
        if (groundCheck.IsGrounded && _jumpVelocity < 0.0f)
            _jumpVelocity = movementConfig.gravityForce;
        else if (_jumpVelocity < 0.0f)
            _jumpVelocity += movementConfig.gravityForce * movementConfig.gravityMultiplier * Time.fixedDeltaTime;
        else
            _jumpVelocity += movementConfig.gravityForce * Time.fixedDeltaTime;

        _targetVelocity.y = _jumpVelocity;
    }

    private void Move()
    {
        _moveDirection = transform.forward * _moveInput.y + transform.right * _moveInput.x;
        _moveDirection.Normalize();

        _targetVelocity = _moveDirection * movementConfig.targetMoveSpeed;
    }

    private void AdjustVelocityToSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, movementConfig.slopeCheckDistance))
        {
            Quaternion slopeRot = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

            Vector3 adjustedVelocity = slopeRot * _targetVelocity;

            if (adjustedVelocity.y < 0.0f)
            {
                _targetVelocity = adjustedVelocity;
            }
        }
    }

    private void Jump()
    {
        if (groundCheck.IsGrounded)
        {
            // Cooldown to ensure that you can still Jump when walking down Ramps/after slightly falling off edge (might not always be Grounded)
            _coyoteTime = movementConfig.coyoteTime;
            _currentJumpHoldTime = movementConfig.maxJumpHoldTime;
            _jumpVelocity = 0.0f;
        }

        if (_coyoteTime > 0.0f)
        {
            _coyoteTime -= Time.fixedDeltaTime;
            _canHold = true;
        }

        if (!_isJumping)
        {
            _jumpBufferTime -= Time.fixedDeltaTime;
            _canHold = false;
            _coyoteTime = -1.0f;
        }

        // Jump
        if (_jumpBufferTime > 0.0f || _canHold)
        {
            // Can Jump as long as Player was recently Grounded
            if (_coyoteTime > 0.0f)
            {
                _coyoteTime = 0.0f;
                _jumpVelocity = movementConfig.initialJumpForce * Time.fixedDeltaTime;
            }

            // Handle Jump Hold
            if (_coyoteTime == 0.0f && _currentJumpHoldTime <= movementConfig.maxJumpHoldTime)
            {
                _jumpVelocity += movementConfig.holdtimeJumpForce * Time.fixedDeltaTime;
                _currentJumpHoldTime -= Time.fixedDeltaTime;
            }

            if (_currentJumpHoldTime < 0.0f)
                _canHold = false;
        }

        if (_jumpBufferTime < 0.0f || !_canHold)
            _currentJumpHoldTime = movementConfig.maxJumpHoldTime;


        _targetVelocity.y = _jumpVelocity;
    }

    private void ApplyAccelDecelRates()
    {
        float rate = _moveInput != Vector2.zero ? movementConfig.accelerationRate : movementConfig.decelerationRate;

        rate = groundCheck.IsGrounded ? rate : rate * movementConfig.airControlFactor;

        // Are we going in the opposite direction?
        if (Vector3.Dot(_moveInput, _previousInput) < 0.0f)
            rate *= movementConfig.rateSwitchDirectionMultiplier;


        float tempY = _targetVelocity.y;

        _targetVelocity = Vector3.MoveTowards(_moveVelocity, _targetVelocity, rate * Time.deltaTime);

        _targetVelocity.y = tempY;


        if (_moveInput != Vector2.zero)
            _previousInput = _moveInput;
    }



    // DESTROY EQUIPPED ITEM METHOD
    /*
    private void EquipWeapon(int weaponIndex)
    {
        if (equippedWeapon != null)
        {
            Destroy(equippedWeapon);
        }

        Weapon weaponToEquip = weaponInventory.ReturnItem(weaponIndex);
        
        equippedWeapon = Instantiate(weaponToEquip, playerHands);
    }
    */

    // DE-ACTIVATE EQUIPPED ITEM METHOD
    private void EquipWeapon(int weaponIndex)
    {
        if (equippedWeapon != null)
            equippedWeapon.transform.gameObject.SetActive(false);

        equippedWeapon = weaponInventory.ReturnItem(weaponIndex);

        if (equippedWeapon == null) return;

        equippedWeapon.transform.gameObject.SetActive(true);
    }




    // For Animator
    public void MeleeAttackStart()
    {
        
    }

    public void MeleeAttackEnd()
    {

    }



    private void UpdateAnimatorVars()
    {
        _animator.SetBool(_hashIsGrounded, groundCheck.IsGrounded);


        float forwardVelocity = Vector3.Project(_targetVelocity, transform.forward).magnitude;
        _animator.SetFloat(_hashForwardVelocity, forwardVelocity);

        _animator.SetBool(_hashIsMovingForward, Vector3.Dot(_targetVelocity, transform.forward) > 0.0f);


        _animator.SetFloat(_hashVerticalVelocity, _controller.velocity.y);


        _animator.SetInteger(_hashRandomIdle, Random.Range(0, 2));


        _animator.ResetTrigger(_hashMeleeAttack);

        // Currently this because other weapons are guns and only anim is for punching/swinging
        if (_inputIsAttacking && equippedWeapon is MeleeWeapon)
            _animator.SetTrigger(_hashMeleeAttack);

        _animator.SetBool(_hashInputDetected, _inputIsLooking || _inputIsAttacking || _inputIsJumping || _moveInput != Vector2.zero);


        AnimatorStateInfo currentAnimStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (currentAnimStateInfo.shortNameHash == _hashMovement || currentAnimStateInfo.shortNameHash == _hashMovementBack)
        {
            if ((_previousAnimState.shortNameHash != _hashMovement && _previousAnimState.shortNameHash != _hashMovementBack) || forwardVelocity > 0.0f || _inputIsLooking)
            {
                _timeToIdle = maxTimeToIdle;
                _animator.SetBool(_hashTimeToIdle, false);
            }

            if (_timeToIdle <= 0.0f)
            {
                _animator.SetBool(_hashTimeToIdle, true);
                _timeToIdle = maxTimeToIdle;
            }
            else
            {
                _timeToIdle -= Time.deltaTime;
            }
        }


        if (_previousAnimState.fullPathHash != currentAnimStateInfo.fullPathHash)
            _previousAnimState = currentAnimStateInfo;
    }
}



enum InputSwitchValues
{
    Attack,
    Jump,
    Look
}