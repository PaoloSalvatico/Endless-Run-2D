using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSpawnableObject : MonoBehaviour
{
    protected Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;

        StartCoroutine("SelfDestruct");
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(13);
        Destroy(gameObject);
    }
}
