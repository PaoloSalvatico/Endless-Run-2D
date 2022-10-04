using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : AbstractSpawnableObject
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
        if(collision.TryGetComponent(out PlayerController player))
        {
            player.CanShoot = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            player.CanShoot = true;
        }
    }
}
