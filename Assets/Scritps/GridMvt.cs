using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMvt : MonoBehaviour
{
    [SerializeField] private float _speed = 2;
    [SerializeField] private float _maxX;
    [SerializeField] private float _increment;
    [SerializeField] private float _timer = 5f;
    private const float EPSILONE = 0.5f;

    private Rigidbody2D _rigidbody2D;

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    public float Timer1
    {
        get => _timer;
        set => _timer = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(moveGrid());
        _rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(Timer());
    }

    private void Update()
    {
        _rigidbody2D.velocity = _increment * _speed * Vector2.right;
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timer);
            _increment *= -1;
        }
    }

}
