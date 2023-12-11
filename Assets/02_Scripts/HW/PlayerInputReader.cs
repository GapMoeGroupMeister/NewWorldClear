using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName = "SO/PlayerInputReader")]
public class PlayerInputReader : ScriptableObject, IPlayerActions
{
    public event Action DashEvent;
    public event Action<Vector2> MoveEvent;
    private Controls _controls;
    public Controls GetControls()
    {
        return _controls;
    }

    private void OnEnable()
    {
        if(_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
        }

        _controls.Player.Enable(); // 입력 활성화
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
            DashEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        MoveEvent?.Invoke(value);
    }
}
