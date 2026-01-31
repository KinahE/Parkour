using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputReader : MonoBehaviour, PlayerControls.IMovementActions
{
    public Vector2 MovementInput { get; private set; }
    public bool IsSprinting { get; private set; }
    public bool IsMovementInput => MovementInput != Vector2.zero;
    private PlayerControls _playerControls;


    public event Action JumpEvent;
    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.Movement.SetCallbacks(this);
        _playerControls.Movement.Enable();

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        JumpEvent?.Invoke();

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        return;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MovementInput = context.ReadValue<Vector2>();
        }
        if (context.canceled)
        {
            MovementInput = Vector2.zero;
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsSprinting = true;
        }
        if (context.canceled)
        {
            IsSprinting = false;
        }
    }
}
