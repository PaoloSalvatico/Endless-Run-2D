using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : AbstractPlayerCommand
{
    protected MoveDirection _direction;
    protected float _speedMultiplier;

    private Rigidbody2D _rb;

    public MoveCommand(MoveDirection direction, float speedMult)
    {
        _direction = direction;
        _speedMultiplier = speedMult;
    }

    public override void Execute(PlayerController player)
    {
        var dir = 1f;
        _rb = player.gameObject.GetComponent<Rigidbody2D>();

        switch (_direction)
        {
            case MoveDirection.Left:
                dir = -1f;
                _rb.velocity = Vector2.right * _speedMultiplier * dir;
                break;
            case MoveDirection.Right:
                dir = 1f;
                _rb.velocity = Vector2.right * _speedMultiplier * dir;
                break;
            case MoveDirection.Up:
                dir = 1f;
                _rb.velocity = Vector2.up * _speedMultiplier * dir;
                break;
            case MoveDirection.Down:
                dir = -1f;
                _rb.velocity = Vector2.up * _speedMultiplier * dir;
                break;
            default:
                break;
        }
    }

    public float SpeedMultiplier
    {
        get { return _speedMultiplier; }
        set { _speedMultiplier = value; }
    }
}

public enum MoveDirection
{
    Left,
    Right,
    Up,
    Down
}