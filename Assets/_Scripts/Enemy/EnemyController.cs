using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Animator _animator;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            _animator.SetTrigger("Hitted");
            _collider.isTrigger = false;
        }

        if(other.TryGetComponent(out PlayerController player))
        {
            Debug.Log("Lose");
            player.SpriteRenderer.sprite = player.HittedSprite;
            // open Lose panel
        }
    }

    public void DestroyEnemy()
    {
        // Add points
        Destroy(gameObject);
    }
}
