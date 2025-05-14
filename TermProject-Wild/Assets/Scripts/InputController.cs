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
    public event Action FireEvent;


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
        
        _gameControls.Player.Jump.performed += OnJumpPerformed;
        
        // etc for look fire jumpcanceled etc
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnMoveCanceled(InputAction.CallbackContext conext)
    {
        MoveEvent?.Invoke(Vector2.zero);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        JumpEvent?.Invoke();
    }

    private void OnDisable()
    {
        _gameControls.Player.Move.performed -= OnMovePerformed;
        _gameControls.Player.Move.canceled -= OnMoveCanceled;
        
        _gameControls.Player.Jump.performed -= OnJumpPerformed;

        _gameControls.Player.Disable();
    }
}
