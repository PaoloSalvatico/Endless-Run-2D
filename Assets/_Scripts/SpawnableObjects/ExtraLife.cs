using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : AbstractSpawnableObject
{
    [SerializeField] private int _extraLife = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            player.GainLife(_extraLife);
            //Instantiate(_enemyHitPlayerVFX, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
