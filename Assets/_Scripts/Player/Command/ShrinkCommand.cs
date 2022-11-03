using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkCommand : AbstractPlayerCommand
{
    private Animator _animator;

    //public ShrinkCommand (Animator animator)
    //{
    //    _animator = animator;
    //}

    public override void Execute(PlayerController player)
    {
        _animator = player.GetComponent<Animator>();
        _animator.SetBool("IsShrinking", true);
    }

    public void StopExecute(PlayerController player)
    {
        _animator = player.GetComponent<Animator>();
        _animator.SetBool("IsShrinking", false);
    }
}
