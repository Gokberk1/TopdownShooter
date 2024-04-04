using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBarEnemy : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Transform _target;
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector3 _offset;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        _slider.value = currentValue / maxValue;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = _camera.transform.rotation;
        transform.position = _target.position + _offset;
    }
}
