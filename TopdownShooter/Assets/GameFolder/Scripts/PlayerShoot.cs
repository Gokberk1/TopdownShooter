using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _crossHair;
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] float _damage = 25;
    [SerializeField] LineRenderer _lineRenderer;
    bool _isShooting;
    [SerializeField] float _fireRate = 0.1f;

    private void Awake()
    {
        //Cursor.visible = false;    
        _muzzleFlash.SetActive(false);
        _lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isShooting)
        {
            StartCoroutine(ShootRaycast());
        }

        Vector2 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _crossHair.transform.position = mouseCursorPos;
    }

    IEnumerator ShootRaycast()
    {
        _muzzleFlash.SetActive(true);
        _isShooting = true;
        


        RaycastHit2D hitInfo = Physics2D.Raycast(_firePoint.position, _firePoint.up);

        if (hitInfo)
        {
            EnemyAI enemy = hitInfo.transform.GetComponent<EnemyAI>();
            
            if(enemy != null)
            {
                enemy.TakeDamage(_damage);
            }

            _lineRenderer.SetPosition(0, _firePoint.position);
            _lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            _lineRenderer.SetPosition(0, _firePoint.position);
            _lineRenderer.SetPosition(1, _firePoint.position + _firePoint.up * 50);
        }

        _lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.02f);

        _lineRenderer.enabled = false;

        yield return new WaitForSeconds(_fireRate);

        _isShooting = false;
        _muzzleFlash.SetActive(false);

    }
}
