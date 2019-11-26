using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        Life obj = other.GetComponent<Life>();
        if (obj)
        {
            obj.Death();

        }
        GameObjectPoolController.Enqueue(GetComponent<Poolable>());
        
    }
    
}
