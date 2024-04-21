using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] float _movementSpeed;
    float _currentHealth;
    [SerializeField] float _maxHealth = 100f;
    [SerializeField] FloatingHealthBar _healthBar;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentHealth = _maxHealth;
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
    }

    private void Update()
    {
        LookAtMouse();
        Movement();
    }

    void LookAtMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = mousePos - new Vector2(transform.position.x, transform.position.y);
    }

    void Movement()
    {
        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _rb.velocity = input * _movementSpeed;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pill"))
        {
            if(_currentHealth <= _maxHealth)
            {
                _currentHealth += 50;
                _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);

                if (_currentHealth > _maxHealth)
                {
                    _currentHealth = _maxHealth;
                }

                Destroy(collision.gameObject);
            }

           
        }
    }
}
