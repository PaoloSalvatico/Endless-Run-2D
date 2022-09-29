using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        _input.Player.Attack.performed += PerformAttack;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Attack.performed -= PerformAttack;
    }

    public Vector2 MoveValue => _input.Player.Movement.ReadValue<Vector2>();

    public delegate void AttackPerformed();
    public AttackPerformed OnAttackPerformed;

    private void PerformAttack(InputAction.CallbackContext context)
    {
        if (OnAttackPerformed == null) return;
        OnAttackPerformed();
    }
}
