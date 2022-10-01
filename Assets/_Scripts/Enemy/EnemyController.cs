using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _enemyPoints;
    [SerializeField] private int _enemyDamage;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;

        StartCoroutine("SelfDestruct");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            _animator.SetTrigger("Hitted");
            _collider.isTrigger = false;
            UIManager.Instance.UpdatePoints(_enemyPoints);
        }

        if(other.TryGetComponent(out PlayerController player))
        {
            player.PlayerHitted(_enemyDamage);
        }
    }

    public void DestroyEnemy()
    {
        // Add points
        Destroy(gameObject);
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(14);
        Destroy(gameObject);
    }
}
