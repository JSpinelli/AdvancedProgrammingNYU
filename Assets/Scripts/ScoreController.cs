using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TextMeshPro _displayedScore;
    private int redScore, blueScore;

    public float timer = 0;
    public const float TimeForGame = 10.0f;

    private void Start()
    {
        Services.EventManager.Register<GoalScored>(IncrementTeamScore);
        Services.EventManager.Register<GameStart>(OnGameStart);
        Services.EventManager.Register<GameEnd>(OnGameEnd);
    }

    private void OnDestroy()
    {
        Services.EventManager.Unregister<GoalScored>(IncrementTeamScore);
    }

    private void Update()
    {

        timer += Time.deltaTime;

        if (timer >= TimeForGame)
        {
            Services.EventManager.Fire(new TimeOut());
        }

        Debug.Log(timer);
        _displayedScore.text = "RED TEAM: " + redScore + "\t\t\t\tBLUE TEAM: " + blueScore;
    }

    private void OnGameStart(AGPEvent e)
    {
        timer = 0;
    }
    
    private void OnGameEnd(AGPEvent e)
    {
        timer = 0;
    }

    private void IncrementTeamScore(AGPEvent e)
    {
        var goalScoredEvent = (GoalScored) e;
        
        if (goalScoredEvent.team1)
        {
            redScore++;
        }
        else
        {
            blueScore++;
        }
            
    }
}
