using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI starGame;
    private int player1Score = 0;
    private int player2Score = 0;

    private float timer = 0;
    private float TimeForGame = 10.0f;

    private void Start()
    {
        Services.EventManager.Register<GoalScored>(IncrementTeamScore);
        Services.EventManager.Register<GameStart>(OnGameStart);
        TimeForGame = Services.gameManager.durationOfMatch;
        starGame.enabled = true;
        score.enabled = false;
        timer = 0;
    }

    private void OnDestroy()
    {
        Services.EventManager.Unregister<GoalScored>(IncrementTeamScore);
        Services.EventManager.Unregister<GameStart>(OnGameStart);
    }

    private void Update()
    {
        if (Services.gameManager._fsm.CurrentState.GetType() != typeof(GameManager.GamePlaying)) return;
        
        timer += Time.deltaTime;
        if (timer >= TimeForGame)
        {
            Services.EventManager.Fire(new GameEnd(player1Score > player2Score));
        }
    }

    private void OnGameStart(AGPEvent e)
    {
        starGame.enabled = false;
        score.enabled = true;
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