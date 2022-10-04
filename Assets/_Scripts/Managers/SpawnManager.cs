using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _obstacle;
    [SerializeField] private float _enemyTimeRangeBetweenSpawn;

    [SerializeField] private GameObject _enemyBoss;
    [SerializeField] private float _enemyBossTimeRangeBetweenSpawn;


    [SerializeField] private GameObject _extraLife;
    [SerializeField] private float _heartTimeRangeBetweenSpawn;

    [SerializeField] private float _minX;
    [SerializeField] private float _minY;
    [SerializeField] private float _maxX;
    [SerializeField] private float _maxY;

    private float _enemySpawnTime = 2f;
    private float _enemyBossSpawnTime = 20f;
    private float _heartsSpawnTime = 15f;

    void Update()
    {
        if (!UIManager.Instance.IsGameGoing) return;
        if(Time.time > _enemySpawnTime)
        {
            var go = _enemy;
            float x = Random.Range(0, 1f);
            if (x > .7f) go = _obstacle;
            Spawn(go);
            _enemySpawnTime = Time.time + _enemyTimeRangeBetweenSpawn;
        }
        //if (Time.time > _enemyBossSpawnTime)
        //{
        //    Spawn(_enemy);
        //    _enemyBossSpawnTime = Time.time + _enemyTimeRangeBetweenSpawn;
        //}
        //if (Time.time > _heartsSpawnTime)
        //{
        //    Spawn(_enemy);
        //    _heartsSpawnTime = Time.time + _enemyTimeRangeBetweenSpawn;
        //}
    }

    private void Spawn(GameObject go)
    {
        float randomX = Random.Range(_minX, _maxX);
        float randomY = Random.Range(_minY, _maxY);

        Instantiate(go, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
    }
}
