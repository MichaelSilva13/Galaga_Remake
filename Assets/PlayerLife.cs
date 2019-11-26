﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : Life
{
    [SerializeField] private int lifeCount = 3;

    public int LifeCount
    {
        get => lifeCount;
        set
        {
            if (value < lifeCount)
            {
                livesImages[lifeCount - 1].enabled = false;
            }else if(lifeCount < livesImages.Length)
            {
                livesImages[lifeCount].enabled = true;
            }
            lifeCount = value;
            
        }
    }

    public bool alive = true;
    public Image[] livesImages;

    private SpriteRenderer _sprite;
    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        livesImages = FindObjectsOfType<Image>();
    }
    
    public override void Death()
    {
        _collider.enabled = false;
        StartCoroutine(DieRoutine());
    }
    
    protected override IEnumerator DieRoutine()
    {
        alive = false;
        Poolable poolable = GameObjectPoolController.Dequeue(_poolableKey);
        poolable.transform.position = transform.position;
        poolable.gameObject.SetActive(true);
        _sprite.enabled = false;
        yield return new WaitForSeconds(0.4f);
        GameObjectPoolController.Enqueue(poolable);
        if (lifeCount > 0)
        {
            LifeCount--;
            alive = true;
            _sprite.enabled = true;
            transform.position = new Vector3(0, -6.5f, -2);
            for (float i = 0; i < 2f; i+=0.1f)
            {
                yield return new WaitForSeconds(0.1f);
                _sprite.enabled = !_sprite.enabled;
            }

            _sprite.enabled = true;
            _collider.enabled = true;
        }
        else
        {
            _sprite.enabled = true;
            _collider.enabled = true;
            alive = true;
            GameObjectPoolController.Enqueue(GetComponent<Poolable>());
            _controller.ChangeState<GameOverState>();
        }
    }

}