using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _detectRange = 7;
    [SerializeField] float _stopingDistance = 1;
    [SerializeField] float _maxHealth = 100f;
    float _currentHealth;
    float _distance;
    float _attackTime;
    [SerializeField] float _attackTimer;
    [SerializeField] float _damage;

    [SerializeField] Animator _anim;
    [SerializeField] GameObject _player;
    [SerializeField] FloatingHealthBarEnemy _healthBar;

    private void Start()
    {
        _healthBar = GetComponentInChildren<FloatingHealthBarEnemy>();
        _currentHealth = _maxHealth;
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
    }

    private void Update()
    {
        _distance = Vector2.Distance(transform.position, _player.transform.position);
        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if(_distance <= _detectRange)
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);

            if(_distance >= _stopingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
            }
            else
            {
                Attack();
            }

        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
        _anim.SetTrigger("TakeDamage");
        if(_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Attack()
    {
        _attackTime += Time.deltaTime;
        if(_attackTime >= _attackTimer)
        {
            _player.GetComponent<PlayerController>().TakeDamage(_damage);
            _attackTime = 0;
        }
    }

    void Die()
    {
        Debug.Log("killed");
    }
}
