using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] float _spawnTime = 5;
    [SerializeField] float _spawnTimer = 0;
    private int _enemyCount;

    private void Update()
    {
        Spawn();
    }

    void Spawn()
    {
        _spawnTimer += Time.deltaTime;

        if(_spawnTimer >= _spawnTime)
        {
            GameObject obj = _objectPool.GetPooledObject();
            obj.transform.position = transform.position;
            _spawnTimer = 0;
        }
    }
}
