using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardState : GameState
{
    private bool inputEnabled;
    public override void Enter()
    {
        base.Enter();
        _owner.LeaderBoardTexts.SetActive(true);
        int i = 0;
        Leaderboard leaderboard = GetComponent<Leaderboard>();
        foreach (Transform child in _owner.LeaderBoardTexts.transform)
        {
            child.GetComponent<Text>().text = leaderboard.scores[i].ToString();
            i++;
        }

        inputEnabled = false;
        StartCoroutine(waitForInput());
    }

    public override void StateUpdate()
    {
        if (inputEnabled)
        {
            if (Input.GetButtonDown("Submit"))
            {
                _owner.ChangeState<HomeState>();
            }
        }
        
    }

    public override void Exit()
    {
        _owner.LeaderBoardTexts.SetActive(false);

    }
    
    IEnumerator waitForInput()
    {
        yield return new WaitForSeconds(1f);
        inputEnabled = true;
    }
}
