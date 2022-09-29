using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Move Stats")]
    [SerializeField] protected float _moveHorizontalMultiplier = 1.5f;
    [SerializeField] protected float _moveVerticalMultiplier = 1.5f;

    [Header("Player Sprites")]
    [SerializeField] private Sprite _idleSprite;
    [SerializeField] private Sprite _hittedSprite;
    [SerializeField] private Sprite _attackSprite;

    [Header("Bullet Stats")]
    [SerializeField] private BulletController _bullet;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private float _bulletSpawnDelay;

    protected MoveCommand _moveLeft;
    protected MoveCommand _moveRight;
    protected MoveCommand _moveUp;
    protected MoveCommand _moveDown;

    protected Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    protected Animator _animator;

    protected bool _canShoot;

    float _inputX = 0;
    float _inputY = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _canShoot = true;

        _moveLeft = new MoveCommand(_rigidbody, MoveDirection.Left, _moveHorizontalMultiplier);
        _moveRight = new MoveCommand(_rigidbody, MoveDirection.Right, _moveHorizontalMultiplier);
        _moveUp = new MoveCommand(_rigidbody, MoveDirection.Up, _moveVerticalMultiplier);
        _moveDown = new MoveCommand(_rigidbody, MoveDirection.Down, _moveVerticalMultiplier);
    }

    private void OnEnable()
    {
        InputManager.Instance.OnAttackPerformed += ShootFire;
    }

    //private void OnDisable()
    //{
    //    InputManager.Instance.OnAttackPerformed -= ShootFire;
    //}

    //public float MoveMultiplier
    //{
    //    get { return _moveHorizontalMultiplier; }
    //    set
    //    {
    //        _moveHorizontalMultiplier = value;
    //        _moveLeft.SpeedMultiplier = value;
    //        _moveRight.SpeedMultiplier = value;
    //    }
    //}

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

    private void ShootFire()
    {
        if(_canShoot)
        {
            _spriteRenderer.flipX = false;
            _spriteRenderer.sprite = _attackSprite;
            Instantiate(_bullet, _bulletSpawnPoint);
            _canShoot = false;
            StartCoroutine("AttackDelay");
        }
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.4f);
        _spriteRenderer.sprite = _idleSprite;
        yield return new WaitForSeconds(_bulletSpawnDelay);
        _canShoot = true;
    }


    public Sprite HittedSprite { get => _hittedSprite; set => _hittedSprite = value; }
    public SpriteRenderer SpriteRenderer { get => _spriteRenderer; set => _spriteRenderer = value; }
}
