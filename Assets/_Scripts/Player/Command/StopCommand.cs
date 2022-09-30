using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCommand : AbstractPlayerCommand
{
    public StopCommand(Rigidbody2D rigidbody) : base(rigidbody)
    {

    }
    public override void Execute()
    {
        _rigidbody.velocity = Vector2.zero;
    }
}
