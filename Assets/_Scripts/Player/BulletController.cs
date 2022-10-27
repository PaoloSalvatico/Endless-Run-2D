using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Rigidbody2D _rigidbody;

    public float BulletSpeed => _bulletSpeed;
    public Rigidbody2D Rigidbody => _rigidbody;

    void Start()
    {
        StartCoroutine("AutoDestroy");
        _rigidbody.AddForce(Vector2.right * _bulletSpeed, ForceMode2D.Impulse);
    }

    private IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
