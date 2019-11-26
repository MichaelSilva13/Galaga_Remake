using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverState : GameState
{
    private bool inputReady = true;
    private NameCursor _cursor;
    private Leaderboard _leaderboard;
    public override void Enter()
    {
        base.Enter();
        _owner.EndScreen.SetActive(true);
        _cursor = FindObjectOfType<NameCursor>();
        _owner.finaleScoreText.text = "FINAL SCORE:\n" + $"{_owner.Score:000,000}";
        Poolable[] poolables = FindObjectsOfType<Poolable>();
        foreach (Poolable poolable in poolables)
        {
            GameObjectPoolController.Enqueue(poolable);
        }

        _leaderboard = GetComponent<Leaderboard>();
    }

    public override void StateUpdate()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.x != 0 || input.y != 0)
        {
            if (inputReady)
            {
                _cursor.MoveCursor((int)input.x);
                _cursor.ChangeLetter((int)input.y);
                inputReady = false;
            }
        }
        else
        {
            inputReady = true;
        }

        if (Input.GetButtonDown("Submit"))
        {
            _owner.ChangeState<LeaderBoardState>();
        }
        
    }

    public override void Exit()
    {
        string name = "";
        foreach (char c in _cursor._name)
        {
            name += c.ToString();
        }
        _leaderboard.AddScore(new PlayerInfo(name, _owner.Score));
        _owner.EndScreen.SetActive(false);

    }
}
