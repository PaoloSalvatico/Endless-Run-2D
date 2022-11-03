using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlayerCommand 
{
    protected Rigidbody2D _rigidbody;

    public AbstractPlayerCommand(Rigidbody2D rigidbody)
    {
        _rigidbody = rigidbody;
    }

    public abstract void Execute(PlayerController player);
}
