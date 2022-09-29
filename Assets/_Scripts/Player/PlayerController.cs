using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Move Stats")]
    [SerializeField] protected float _moveMultiplier = 1.5f;
    [SerializeField] protected float _jumpMultiplier = 5f;

    protected MoveCommand _moveLeft;
    protected MoveCommand _moveRight;
    protected MoveCommand _moveUp;
    protected MoveCommand _moveDown;

    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;
    protected Animator _animator;

    float _inputX = 0;
    float _inputY = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        _moveLeft = new MoveCommand(_rigidbody, MoveDirection.Left, _moveMultiplier);
        _moveRight = new MoveCommand(_rigidbody, MoveDirection.Right, _moveMultiplier);
        _moveUp = new MoveCommand(_rigidbody, MoveDirection.Up, _moveMultiplier);
        _moveDown = new MoveCommand(_rigidbody, MoveDirection.Down, _moveMultiplier);
    }

    public float MoveMultiplier
    {
        get { return _moveMultiplier; }
        set
        {
            _moveMultiplier = value;
            _moveLeft.SpeedMultiplier = value;
            _moveRight.SpeedMultiplier = value;
        }
    }

    void Update()
    {
        _inputX = InputManager.Instance.MoveValue.x;
        _inputY = InputManager.Instance.MoveValue.y;

        
    }

    private void FixedUpdate()
    {
        if (_inputX > 0)
        {
            _moveRight.Execute();
            _spriteRenderer.flipX = false;
        }
        else if (_inputX < 0)
        {
            _moveLeft.Execute();
            _spriteRenderer.flipX = true;
        }

        if (_inputY > 0)
        {
            _moveUp.Execute();
        }
        else if (_inputY < 0)
        {
            _moveDown.Execute();
        }
    }
}
