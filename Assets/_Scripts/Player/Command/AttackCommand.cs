using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : AbstractPlayerCommand
{
    [SerializeField] private float _speedMultiplier;

    private PlayerController _player;

    public override void Execute(PlayerController player)
    {
        player.ShootFire();
    }

    public AttackCommand(Rigidbody2D rigidbody) : base(rigidbody)
    {
        //_player = player;
        //speedMult = _speedMultiplier;
    }
}
