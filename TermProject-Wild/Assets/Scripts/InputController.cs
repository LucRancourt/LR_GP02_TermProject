using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    // Variables
    private GameControls _gameControls;
    
    
    // Events + Methods
    public event Action<Vector2> MoveEvent;
    
    public event Action<Vector2> LookEvent;
    
    public event Action JumpEvent;
    public event Action JumpCancelEvent; // For variable jump height as an example

    public event Action SlideEvent;
    
    public event Action CrouchEvent;
    public event Action CrouchCancelEvent;

    public event Action<int> EquipEvent;
    
    public event Action FireEvent;
    public event Action FireCancelEvent;

    public event Action ReloadEvent;


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
        
        _gameControls.Player.Slide.performed += OnSlidePerformed;

        _gameControls.Player.Crouch.performed += OnCrouchPerformed;
        _gameControls.Player.Crouch.canceled += OnCrouchCanceled;
        
        _gameControls.Player.Jump.performed += OnJumpPerformed;
        _gameControls.Player.Jump.canceled += OnJumpCanceled;

        _gameControls.Player.Look.performed += OnLookPerformed;

        _gameControls.Player.EquipItem.performed += OnEquipPerformed;
        
        _gameControls.Player.Fire.performed += OnFirePerformed;
        _gameControls.Player.Fire.canceled += OnFireCanceled;

        _gameControls.Player.Reload.performed += OnReloadPerformed;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnMoveCanceled(InputAction.CallbackContext conext)
    {
        MoveEvent?.Invoke(Vector2.zero);
    }

    private void OnSlidePerformed(InputAction.CallbackContext context)
    {
        SlideEvent?.Invoke();
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
        EquipEvent?.Invoke(context.ReadValue<int>());
    }

    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        FireEvent?.Invoke();
    }

    private void OnFireCanceled(InputAction.CallbackContext context)
    {
        FireCancelEvent?.Invoke();
    }

    private void OnReloadPerformed(InputAction.CallbackContext context)
    {
        ReloadEvent?.Invoke();
    }

    private void OnDisable()
    {
        _gameControls.Player.Move.performed -= OnMovePerformed;
        _gameControls.Player.Move.canceled -= OnMoveCanceled;
        
        _gameControls.Player.Slide.performed -= OnSlidePerformed;

        _gameControls.Player.Crouch.performed -= OnCrouchPerformed;
        _gameControls.Player.Crouch.canceled -= OnCrouchCanceled;
        
        _gameControls.Player.Jump.performed -= OnJumpPerformed;
        _gameControls.Player.Jump.canceled -= OnJumpCanceled;

        _gameControls.Player.Look.performed -= OnLookPerformed;

        _gameControls.Player.EquipItem.performed -= OnEquipPerformed;
        
        _gameControls.Player.Fire.performed -= OnFirePerformed;
        _gameControls.Player.Fire.canceled -= OnFireCanceled;

        _gameControls.Player.Reload.performed -= OnReloadPerformed;

        _gameControls.Player.Disable();
    }
}
