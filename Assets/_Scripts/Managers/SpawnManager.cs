using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _obstacle;
    [SerializeField] private float _timeRangeBetweenSpawn;

    [SerializeField] private float _minX;
    [SerializeField] private float _minY;
    [SerializeField] private float _maxX;
    [SerializeField] private float _maxY;

    private float _spawnTime = 2f;

    void Update()
    {
        if (!UIManager.Instance.IsGameGoing) return;
        if(Time.time > _spawnTime)
        {
            Spawn();
            _spawnTime = Time.time + _timeRangeBetweenSpawn;
        }
    }

    private void Spawn()
    {
        float randomX = Random.Range(_minX, _maxX);
        float randomY = Random.Range(_minY, _maxY);

        Instantiate(_enemy, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
    }
}
