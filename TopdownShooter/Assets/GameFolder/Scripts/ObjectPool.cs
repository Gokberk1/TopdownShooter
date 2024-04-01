using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> _pooledObjects;
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private int _poolSize;

    private void Awake()
    {
        _pooledObjects = new Queue<GameObject>();
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(_objectPrefab);
            obj.SetActive(false);
            _pooledObjects.Enqueue(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        GameObject obj = _pooledObjects.Dequeue();
        obj.SetActive(true);
        _pooledObjects.Enqueue(obj);
        return obj;
    }
}
