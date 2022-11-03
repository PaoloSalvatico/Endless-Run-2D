using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] [Range(0, 16)] protected int _lifePoints;

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

    protected StopCommand _stopMovement;

    protected ShrinkCommand _shrinkCommand;

    protected AttackCommand _attackCommand;

    protected Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    protected Animator _animator;

    protected DataContainer _data;

    protected bool _canShoot;
    protected bool _isShrinked;

    float _inputX = 0;
    float _inputY = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _canShoot = true;

        _moveLeft = new MoveCommand(_rigidbody, MoveDirection.Left, _moveHorizontalMultiplier);
        _moveRight = new MoveCommand(_rigidbody, MoveDirection.Right, _moveHorizontalMultiplier);
        _moveUp = new MoveCommand(_rigidbody, MoveDirection.Up, _moveVerticalMultiplier);
        _moveDown = new MoveCommand(_rigidbody, MoveDirection.Down, _moveVerticalMultiplier);

        _stopMovement = new StopCommand(_rigidbody);
        _shrinkCommand = new ShrinkCommand(_rigidbody, _animator);
        _attackCommand = new AttackCommand(_rigidbody);
    }

    private void OnEnable()
    {
        InputManager.Instance.OnAttackPerformed += ShootFire;
        InputManager.Instance.OnStopPerformed += StopMovement;
        InputManager.Instance.OnStartShrink += Shrink;
        InputManager.Instance.OnStopShrink += StopShrink;
    }

    private void OnDisable()
    {
        //InputManager.Instance.OnAttackPerformed -= ShootFire;
        InputManager.Instance.OnStopPerformed -= StopMovement;
        InputManager.Instance.OnStartShrink -= Shrink;
        InputManager.Instance.OnStopShrink -= StopShrink;
    }

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
        if (!UIManager.Instance.IsGameGoing) return;
        if (_inputX > 0)
        {
            _moveRight.Execute(this);
            _spriteRenderer.flipX = false;
        }
        else if (_inputX < 0)
        {
            _moveLeft.Execute(this);
            _spriteRenderer.flipX = true;
        }

        if (_inputY > 0)
        {
            _moveUp.Execute(this);
        }
        else if (_inputY < 0)
        {
            _moveDown.Execute(this);
        }
    }

    private void DefaultShootCommand()
    {
        _attackCommand.Execute(this);
    }

    public void ShootFire()
    {
        if(_canShoot && !_isShrinked)
        {
            _spriteRenderer.flipX = false;
            SetPlayerSprite(_attackSprite);
            Instantiate(_bullet, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
            _canShoot = false;
            StartCoroutine("AttackDelay");
        }
    }

    private IEnumerator AttackDelay()
    {
        StartCoroutine("ResetIdleSprite");
        yield return new WaitForSeconds(_bulletSpawnDelay);
        _canShoot = true;
    }

    public void PlayerHitted(int damage)
    {
        SetPlayerSprite(_hittedSprite);
        DestroyHearts(damage);

        if(_lifePoints <= 0)
        {
            // TODO Add open lose panel
            UIManager.Instance.IsGameGoing = false;
            UIManager.Instance.SaveRecordPoints();
            UIManager.Instance.OpenLosePanel();
            Destroy(gameObject);
            return;
        }
        StartCoroutine("ResetIdleSprite");
    }

    public void GainLife(int life)
    {
        _lifePoints += life;
        UIManager.Instance.UpdateHeartsUI();
    }

    private IEnumerator ResetIdleSprite()
    {
        yield return new WaitForSeconds(.4f);
        SetPlayerSprite(_idleSprite);
    }

    public void SetPlayerSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    private void StopMovement()
    {
        _stopMovement.Execute(this);
    }

    private void Shrink()
    {
        _shrinkCommand.Execute(this);
        _canShoot = false;
        _isShrinked = true;
    }

    private void StopShrink()
    {
        _shrinkCommand.StopExecute();
        _canShoot = true;
        _isShrinked = false;
    }

    private void DestroyHearts(int damage)
    {
        for(int i = 0; i < damage; i++)
        {
            _lifePoints -= 1;
            var heart = UIManager.Instance.PlayerHeartList[_lifePoints];
            Destroy(heart);
            if (_lifePoints <= 0) break;
        }
    }

    public int LifePoints => _lifePoints;

    public bool CanShoot { get => _canShoot; set => _canShoot = value; }
}
