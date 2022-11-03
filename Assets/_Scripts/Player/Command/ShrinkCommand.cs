using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkCommand : AbstractPlayerCommand
{
    protected Animator _animator;

    public ShrinkCommand (Rigidbody2D rigidbody, Animator animator) : base(rigidbody)
    {
        _animator = animator;
    }

    public override void Execute(PlayerController player)
    {
        _animator.SetBool("IsShrinking", true);
    }

    public void StopExecute()
    {
        _animator.SetBool("IsShrinking", false);
    }
}
