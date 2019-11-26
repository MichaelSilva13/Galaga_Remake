using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    protected GameController _owner;
    public abstract void StateUpdate();

    public virtual void Enter()
    {
        _owner = GetComponent<GameController>();
    }
    
    public abstract void Exit();
}
