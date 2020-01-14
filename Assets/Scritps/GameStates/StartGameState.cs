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
        PlayerLife life = player.GetComponent<PlayerLife>();
        if(life.LifeCount < 3){
            for (int i = life.LifeCount; i < 3; i++)
            {
                life.LifeCount++;
            }
        }

        _owner.alive1 = true;
        player.transform.position = new Vector3(0, -6.5f, -2);
        if (_owner.player2)
        {
            _owner.LifeCounter2.SetActive(true);
            GameObject player2 = GameObjectPoolController.Dequeue("Player2").gameObject;
            player2.SetActive(true);
            PlayerLife life2 = player2.GetComponent<PlayerLife>();
            if(life2.LifeCount < 3){
                for (int i = life2.LifeCount; i < 3; i++)
                {
                    life2.LifeCount++;
                }
            }
        
            player2.transform.position = new Vector3(0, -6.5f, -2);
            _owner.alive2 = true;
        }
        _enemySpawner = FindObjectOfType<EnemySpawner>();
        _enemySpawner.StartGame();
    }

    public override void Exit()
    {
        FindObjectOfType<EnemySpawner>().gameON = false;
    }
    
    
}
