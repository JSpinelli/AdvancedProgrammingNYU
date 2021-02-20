using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI starGame;
    public TextMeshProUGUI winner;
    public TextMeshProUGUI timerDisplay;
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
        winner.enabled = false;
        timerDisplay.enabled = false;
        timer = 0;
    }

    private void OnDestroy()
    {
        Services.EventManager.Unregister<GoalScored>(IncrementTeamScore);
        Services.EventManager.Unregister<GameStart>(OnGameStart);
    }

    private void Update()
    {
        if (!Services.gameManager.IsPlaying()) return;

        timer += Time.deltaTime;
        timerDisplay.text = (TimeForGame - timer).ToString("F1");
        if (timer >= TimeForGame)
        {
            Services.EventManager.Fire(new GameEnd(player1Score > player2Score));
            OnGameEnd();
        }
    }

    private void OnGameStart(AGPEvent e)
    {
        starGame.enabled = false;
        score.enabled = true;
        winner.enabled = false;
        timerDisplay.enabled = true;
        timer = 0;
        player1Score = 0;
        player2Score = 0;
    }

    private void OnGameEnd()
    {
        starGame.enabled = true;
        score.enabled = false;
        winner.enabled = true;
        timerDisplay.enabled = false;
        winner.text = player1Score > player2Score ? "Player 1 is the winner" : "Player 2 is the winner";
        timer = 0;
        player1Score = 0;
        player2Score = 0;
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