using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI score;
    private int player1Score = 0;
    private int player2Score = 0;

    private float timer = 0;
    private float TimeForGame = 10.0f;

    private void Start()
    {
        Services.EventManager.Register<GoalScored>(IncrementTeamScore);
        Services.EventManager.Register<GameStart>(OnGameStart);
        Services.EventManager.Register<GameEnd>(OnGameEnd);
        TimeForGame = Services.gameManager.durationOfMatch;
        timer = 0;
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
        var team1 = ((GoalScored) e).team1;
        
        if (team1)
        {
            player1Score++;
        }
        else
        {
            player2Score++;
        }

        score.text = player1Score + " - " + player2Score;
            
    }
}
