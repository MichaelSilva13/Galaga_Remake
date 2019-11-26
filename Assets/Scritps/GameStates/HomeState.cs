using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeState : GameState
{

    private bool inputEnabled = false;

    public override void StateUpdate()
    {
        if (_owner.credits > 0 && Input.GetButtonDown("Submit"))
        {
            _owner.ChangeState<StartGameState>();
        }
    }

    public override void Enter()
    {
        base.Enter();
        StartCoroutine(waitForInput());
        _owner.homeSound.Play();
        _owner.Score = 0;
        _owner.Level = 1;
        _owner.WelcomeScreen.SetActive(true);
        _owner.LifeCounter.SetActive(false);
    }

    public override void Exit()
    {
        _owner.credits--;
        _owner.PressStart.SetActive(false);
        inputEnabled = false;
    }

    IEnumerator waitForInput()
    {
        yield return new WaitForSeconds(1f);
        inputEnabled = true;
    }
}
