using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInput : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private PlayerLife _life;

    [SerializeField] private float _speed = 7f;

    [SerializeField] private GameObject _missile;

    [SerializeField] private float _missileSpeed = 20f;

    [SerializeField] private GameObject _explosion;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _life = GetComponent<PlayerLife>();
        GameObjectPoolController.AddEntry("player_missile", _missile, 10, 50);
        GameObjectPoolController.AddEntry("playerExplo", _explosion, 10, 100);
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal") * _speed;
        float y = Input.GetAxisRaw("Vertical") * _speed;
        
        Vector2 mvt = new Vector2(x, y);
        _rigidbody.velocity = mvt;
    }

    private void LateUpdate()
    {
        if (Input.GetButtonDown("Fire1") && _life.alive)
        {
            GameObject projectile = GameObjectPoolController.Dequeue("player_missile").gameObject;
            projectile.SetActive(true);
            projectile.transform.position = transform.position + Vector3.up;
            projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, _missileSpeed), ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
