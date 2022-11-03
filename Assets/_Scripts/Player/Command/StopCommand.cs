using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCommand : AbstractPlayerCommand
{
    //public StopCommand(Rigidbody2D rigidbody)
    //{

    //}
    public override void Execute(PlayerController player)
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
