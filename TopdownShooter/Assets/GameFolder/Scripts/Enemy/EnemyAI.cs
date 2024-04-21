using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _stopingDistance = 1;
    float _distance;
    [SerializeField] float _detectRange;
    [SerializeField] float _maxHealth = 100f;
    float _currentHealth;
    float _attackTime;
    [SerializeField] float _attackTimer;
    [SerializeField] float _damage;
    GameObject _player;
    [SerializeField] Animator _anim;
    [SerializeField] FloatingHealthBar _healthBar;
    [SerializeField] SpriteRenderer _bodySprite;
    Rigidbody2D _rb;

    [SerializeField] GameObject _lootItem;


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
        _healthBar = GetComponentInChildren<FloatingHealthBar>();
        _currentHealth = _maxHealth;
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
    }


    private void Update()
    {
        _distance = Vector2.Distance(transform.position, _player.transform.position);

        //if(_distance <= _detectRange)
        //{
            Vector2 direction = _player.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);

            if (_distance >= _stopingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
            }
            else
            {
                Attack();
            }

            if (_distance <= _stopingDistance)
            {
                Attack();
            }
        //}
       

        
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
        _currentHealth = _maxHealth;
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
        _bodySprite.color = Color.white;
        gameObject.SetActive(false);

        if(Random.Range(1, 100) >= 70)
        {
            Instantiate(_lootItem, transform.position, transform.rotation);
        }
    }
}
