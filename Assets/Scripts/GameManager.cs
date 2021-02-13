using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject ball;

    public TextMeshProUGUI score;

    private int player1Score = 0;
    private int player2Score = 0;

    public float movementSpeed = 0.01f;

    private Vector3 startingZone;
    // Start is called before the first frame update
    void Start()
    {
        startingZone = ball.transform.position;
        _InitializeServices();
    }

    // Update is called once per frame
    void Update()
    {
        Services.player1.Move();
        Services.player2.Move();
    }

    _InitializeServices() {
		Services.GameManager = this;
        Services.EventManager = new EventManager();
        Services.EventManager.Register<GoalScored>(OnGoalScored);

        Services.player1 = new PlayerControlled(player1, 10f, ball);
        Services.player2 = new ForcePlayer(player2, 0.01f, ball);
	}
    
    public void OnGoalScored(AGPEvent e)
    {
        var goalScoredWasBlue = ((GoalScored) e).blueTeamScored;
        
        Debug.Log("Goal was blue: " + goalScoredWasBlue);
    }




    public void Score(string player){
        if (player == "Player 1"){
            player2Score++;
        }else{
            player1Score++;
        }
        score.text = player1Score+" - "+player2Score;
        ball.transform.position = new Vector3();
    }

    public void Restart(){
        ball.transform.position = startingZone;
    }
}
