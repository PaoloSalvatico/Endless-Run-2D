using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : AbstractPlayerCommand
{
    protected MoveDirection _direction;
    protected float _speedMultiplier;

    public MoveCommand(Rigidbody2D rigidbody, MoveDirection direction, float speedMult) : base(rigidbody)
    {
        _direction = direction;
        _speedMultiplier = speedMult;
    }

    public override void Execute()
    {
        var dir = 1f;

        switch(_direction)
        {
            case MoveDirection.Left:
                dir = -1f;
                _rigidbody.velocity = Vector2.right * _speedMultiplier * dir;
                break;
            case MoveDirection.Right:
                dir = 1f;
                _rigidbody.velocity = Vector2.right * _speedMultiplier * dir;
                break;
            case MoveDirection.Up:
                dir = 1f;
                _rigidbody.velocity = Vector2.up * _speedMultiplier * dir;
                break;
            case MoveDirection.Down:
                dir = -1f;
                _rigidbody.velocity = Vector2.up * _speedMultiplier * dir;
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