using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI _pointText;
    [SerializeField] private Sprite _playerHearts;
    [SerializeField] private GameObject _playerHeartContainer;
    [SerializeField] private Transform _playerHeartsPos;
    [SerializeField] private PlayerController _player;

    private float _addPoints;
    private float _points;

    private void Start()
    {
        for(int i = 0; i < _player.LifePoints; i++)
        {
            var position = new Vector3(_playerHeartsPos.position.x + i * 3, _playerHeartsPos.position.y, 0);
            var go = Instantiate(_playerHeartContainer, position , _playerHeartsPos.rotation);
            //go.GetComponent<SpriteRenderer>().sprite = _playerHearts;
        }
    }

    public void UpdatePoints(float points)
    {
        _addPoints = points;
    }

    void Update()
    {
        _points += Time.deltaTime * 10 + _addPoints;
        if (_addPoints > 0) _addPoints = 0;
        int newPoints = (int)_points;
        _pointText.text = "Points: " + newPoints.ToString();
    }
}
