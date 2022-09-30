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
        _input.Player.StopMovement.performed += StopMovement;
        _input.Player.Shrink.started += Shrink;
        _input.Player.Shrink.canceled += BackNormal;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Attack.performed -= PerformAttack;
        _input.Player.StopMovement.performed -= StopMovement;
        _input.Player.Shrink.started -= Shrink;
        _input.Player.Shrink.canceled -= BackNormal;
    }

    public Vector2 MoveValue => _input.Player.Movement.ReadValue<Vector2>();

    public delegate void StopPerformed();
    public StopPerformed OnStopPerformed;

    private void StopMovement(InputAction.CallbackContext context)
    {
        if (OnStopPerformed == null) return;
        OnStopPerformed();
    }

    public delegate void AttackPerformed();
    public AttackPerformed OnAttackPerformed;

    private void PerformAttack(InputAction.CallbackContext context)
    {
        if (OnAttackPerformed == null) return;
        OnAttackPerformed();
    }

    public delegate void StartShrink();
    public StartShrink OnStartShrink;

    private void Shrink(InputAction.CallbackContext context)
    {
        if (OnStartShrink == null) return;
        OnStartShrink();
    }

    public delegate void StopShrink();
    public StopShrink OnStopShrink;

    private void BackNormal(InputAction.CallbackContext context)
    {
        if (OnStopShrink == null) return;
        OnStopShrink();
    }
}
