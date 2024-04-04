using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    Rigidbody2D _rbBullet;
    [SerializeField] GameObject _hitParticle;

    private void Start()
    {
        _rbBullet = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject particle = Instantiate(_hitParticle, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        Destroy(particle.gameObject, 1);
    }
}
