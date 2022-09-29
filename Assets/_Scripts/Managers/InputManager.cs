using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private GameInput _input;

    protected override void Awake()
    {
        base.Awake();
        _input = new GameInput();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    public Vector2 MoveValue => _input.Player.Movement.ReadValue<Vector2>();
}
