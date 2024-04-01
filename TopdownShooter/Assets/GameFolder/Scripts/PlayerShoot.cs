using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private Transform _firePoint;
    bool _isShooting;
    float _fireRate = 0.3f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isShooting)
        {
            StartCoroutine(Shooting());
        }
    }

    IEnumerator Shooting()
    {
        _isShooting = true;
        GameObject obj = _objectPool.GetPooledObject();
        obj.transform.position = _firePoint.position;
        obj.transform.rotation = _firePoint.rotation;
        yield return new WaitForSeconds(_fireRate);
        _isShooting = false;
    }
}
