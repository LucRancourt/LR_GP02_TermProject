using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    // Variables
    private GameControls _gameControls;


    // Events + Methods
    #region Actions
    public event Action<Vector2> MoveEvent;
    
    public event Action<Vector2> LookEvent;
    
    public event Action JumpEvent;
    public event Action JumpCancelEvent; // For variable jump height as an example

    public event Action BlinkEvent;
    
    public event Action CrouchEvent;
    public event Action CrouchCancelEvent;

    public event Action<float> EquipEvent;
    
    public event Action FireEvent;
    public event Action FireCancelEvent;

    public event Action AimEvent;
    public event Action AimCancelEvent;

    public event Action ReloadEvent;

    public event Action InteractEvent;

    public event Action SwitchPOVEvent;
    #endregion


    // Functions
    private void Awake()
    {
        _gameControls = new GameControls();
    }

    private void OnEnable()
    {
        _gameControls.Player.Enable();

        _gameControls.Player.Move.performed += OnMovePerformed;
        _gameControls.Player.Move.canceled += OnMoveCanceled;
        
        _gameControls.Player.Blink.performed += OnBlinkPerformed;

        _gameControls.Player.Crouch.performed += OnCrouchPerformed;
        _gameControls.Player.Crouch.canceled += OnCrouchCanceled;
        
        _gameControls.Player.Jump.performed += OnJumpPerformed;
        _gameControls.Player.Jump.canceled += OnJumpCanceled;

        _gameControls.Player.Look.performed += OnLookPerformed;

        _gameControls.Player.EquipItem.performed += OnEquipPerformed;
        
        _gameControls.Player.Fire.performed += OnFirePerformed;
        _gameControls.Player.Fire.canceled += OnFireCanceled;

        _gameControls.Player.Aim.performed += OnAimPerformed;
        _gameControls.Player.Aim.canceled += OnAimCanceled;

        _gameControls.Player.Reload.performed += OnReloadPerformed;

        _gameControls.Player.Interact.performed += OnInteractPerformed;

        _gameControls.Player.SwitchPOV.performed += OnSwitchPOVPerformed;
    }

    #region Handlers
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnMoveCanceled(InputAction.CallbackContext conext)
    {
        MoveEvent?.Invoke(Vector2.zero);
    }

    private void OnBlinkPerformed(InputAction.CallbackContext context)
    {
        BlinkEvent?.Invoke();
    }

    private void OnCrouchPerformed(InputAction.CallbackContext context)
    {
        CrouchEvent?.Invoke();
    }

    private void OnCrouchCanceled(InputAction.CallbackContext context)
    {
        CrouchCancelEvent?.Invoke();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        JumpEvent?.Invoke();
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        JumpCancelEvent?.Invoke();
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        LookEvent?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnEquipPerformed(InputAction.CallbackContext context)
    {
        EquipEvent?.Invoke(context.ReadValue<float>());
    }

    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        FireEvent?.Invoke();
    }

    private void OnFireCanceled(InputAction.CallbackContext context)
    {
        FireCancelEvent?.Invoke();
    }

    private void OnAimPerformed(InputAction.CallbackContext context)
    {
        AimEvent?.Invoke();
    }

    private void OnAimCanceled(InputAction.CallbackContext context)
    {
        AimCancelEvent?.Invoke();
    }

    private void OnReloadPerformed(InputAction.CallbackContext context)
    {
        ReloadEvent?.Invoke();
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        InteractEvent?.Invoke();
    }

    private void OnSwitchPOVPerformed(InputAction.CallbackContext context)
    {
        SwitchPOVEvent?.Invoke();
    }
    #endregion

    private void OnDisable()
    {
        _gameControls.Player.Move.performed -= OnMovePerformed;
        _gameControls.Player.Move.canceled -= OnMoveCanceled;
        
        _gameControls.Player.Blink.performed -= OnBlinkPerformed;

        _gameControls.Player.Crouch.performed -= OnCrouchPerformed;
        _gameControls.Player.Crouch.canceled -= OnCrouchCanceled;
        
        _gameControls.Player.Jump.performed -= OnJumpPerformed;
        _gameControls.Player.Jump.canceled -= OnJumpCanceled;

        _gameControls.Player.Look.performed -= OnLookPerformed;

        _gameControls.Player.EquipItem.performed -= OnEquipPerformed;
        
        _gameControls.Player.Fire.performed -= OnFirePerformed;
        _gameControls.Player.Fire.canceled -= OnFireCanceled;

        _gameControls.Player.Aim.performed -= OnAimPerformed;
        _gameControls.Player.Aim.canceled -= OnAimCanceled;

        _gameControls.Player.Reload.performed -= OnReloadPerformed;

        _gameControls.Player.Interact.performed -= OnInteractPerformed;

        _gameControls.Player.SwitchPOV.performed -= OnSwitchPOVPerformed;

        _gameControls.Player.Disable();
    }
}
