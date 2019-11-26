using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameState : GameState
{
    protected EnemySpawner _enemySpawner;
    
    public override void StateUpdate()
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        _owner.LifeCounter.SetActive(true);
        _owner.homeSound.Stop();
        _owner.startSound.Play();
        GameObject player = GameObjectPoolController.Dequeue("Player").gameObject;
        player.SetActive(true);
        PlayerLife life = FindObjectOfType<PlayerLife>();
        if(life.LifeCount < 3){
            for (int i = life.LifeCount; i < 3; i++)
            {
                life.LifeCount++;
            }
        }
        
        player.transform.position = new Vector3(0, -6.5f, -2);
        _enemySpawner = FindObjectOfType<EnemySpawner>();
        _enemySpawner.StartGame();
    }

    public override void Exit()
    {
        FindObjectOfType<EnemySpawner>().gameON = false;
    }
    
    
}
