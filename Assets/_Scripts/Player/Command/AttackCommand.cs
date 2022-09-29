using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : AbstractPlayerCommand
{
    [SerializeField] private float _speedMultiplier;

    public override void Execute()
    {
        
    }

    public AttackCommand(Rigidbody2D rigidbody, float speedMult) : base(rigidbody)
    {
        speedMult = _speedMultiplier;
    }
}
