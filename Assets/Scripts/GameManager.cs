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
    
    public int PlayersPerTeam = 0;

    public float movementSpeed = 0.01f;
    private Vector3 startingZone;
    
    void Start()
    {
        startingZone = ball.transform.position;
        _InitializeServices();
    }

    // Update is called once per frame
    void Update()
    {
        // Services.player1.Move();
        // Services.player2.Move();
        
        Services.Input.Update();
        Services.Players[0].Update();
        Services.AIManager.Update();
    }
    
    private void OnDestroy()
    {
        Services.AIManager.Destroy();
    }

    void _InitializeServices() {
		Services.gameManager = this;
        Services.EventManager = new EventManager();
        Services.EventManager.Register<GoalScored>(OnGoalScored);
        
        Services.AIManager = new AIController();
        Services.AIManager.Initialize();
        
        Services.Input = new InputManager();
        
        var playerGameObject = Instantiate(Resources.Load<GameObject>("Player"));
        // Services.Players = new[] {new UserControlledPlayer(playerGameObject, new [] {KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D })};
        Services.Players = new[] {new UserControlledPlayer(playerGameObject)};
 
        Services.player1 = new PlayerControlled(player1, 10f, ball);
        Services.player2 = new ForcePlayer(player2, 0.01f, ball);
	}
    
    public void OnGoalScored(AGPEvent e)
    {
        var goalScoredWasBlue = ((GoalScored) e).team1;
        
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
