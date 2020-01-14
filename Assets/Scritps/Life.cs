using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] protected string _poolableKey;
    protected Collider2D _collider;
    protected EnemySpawner _spawner;
    public int pointValue = 100;
    protected GameController _controller;

    private void Awake()
    {
        if (!_collider)
        {
            _collider = GetComponent<Collider2D>();
        }
        _collider.enabled = true;
        _spawner = FindObjectOfType<EnemySpawner>();
        _controller = FindObjectOfType<GameController>();
    }

    public virtual void Death()
    {
        _collider.enabled = false;
        StartCoroutine(DieRoutine());
        _spawner._enemyAmmount--;
        _spawner.clonesPresent[GetComponent<Poolable>().key]--;
        _controller.Score += pointValue;
        GetComponent<EnemyMvt>().attackPath.transform.parent = transform;
        FindObjectOfType<EnemyGrid>().FreeSpace(GetComponent<EnemyMvt>().finalPosition);
    }

    protected virtual IEnumerator DieRoutine()
    {
        Poolable poolable = GameObjectPoolController.Dequeue(_poolableKey);
        poolable.transform.position = transform.position;
        poolable.gameObject.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.4f);
        GameObjectPoolController.Enqueue(poolable);
        GameObjectPoolController.Enqueue(GetComponent<Poolable>());
    }
}
