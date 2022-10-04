using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [Header("Score Management")]
    [SerializeField] private TextMeshProUGUI _pointText;
    [SerializeField] private TextMeshProUGUI _recordPointText;

    [Header("Lose Panel")]
    [SerializeField] private GameObject _losepanel;
    [SerializeField] private TextMeshProUGUI _losePanelActualPoints;

    [Header("Player Stats")]
    [SerializeField] private Image _playerHearts;
    [SerializeField] private Transform _playerHeartsPos;
    [SerializeField] private PlayerController _player;

    private float _addPoints;
    private float _points;
    private bool _isGameGoing;
    private List<GameObject> _playerHeartList;

    protected override void Awake()
    {
        base.Awake();
        _isGameGoing = true;
        _playerHeartList = new List<GameObject>();
    }

    private void Start()
    {
        UpdateRecordPointText(VariableSaver.Instance.RecordPoints);

        for (int i = 0; i < _player.LifePoints; i++)
        {
            var position = new Vector3(_playerHeartsPos.position.x + i * -50, _playerHeartsPos.position.y, 0);
            var playerHeart = Instantiate(_playerHearts.gameObject, position, _playerHeartsPos.rotation, _playerHeartsPos);
            _playerHeartList.Add(playerHeart);
        }
    }

    public void UpdatePoints(float points)
    {
        _addPoints = points;
    }

    public void SaveRecordPoints()
    {
        int recordPoints = VariableSaver.Instance.RecordPoints;
        if (recordPoints > _points) return;

        recordPoints = (int)_points;
        VariableSaver.Instance.RecordPoints = recordPoints;
        UpdateRecordPointText(recordPoints);
    }

    public void UpdateRecordPointText(int points)
    {
        _recordPointText.text = "Record: " + points.ToString();
    }

    void Update()
    {
        if (!_isGameGoing) return;
        _points += Time.deltaTime * 10 + _addPoints;
        if (_addPoints > 0) _addPoints = 0;
        int newPoints = (int)_points;
        _pointText.text = "Points: " + newPoints.ToString();
    }

    public float Points => _points;
    public bool IsGameGoing { get => _isGameGoing; set => _isGameGoing = value; }
    public List<GameObject> PlayerHeartList { get => _playerHeartList; set => _playerHeartList = value; }
}
