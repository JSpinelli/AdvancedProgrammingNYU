using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject ball;

    public int PlayersPerTeam = 0;

    public float movementSpeed = 0.01f;

    public float durationOfMatch = 10f;
    
    private Vector3 startingZone;

    void _InitializeServices()
    {
        Services.gameManager = this;
        Services.EventManager = new EventManager();
        Services.EventManager.Register<GoalScored>(OnGoalScored);
        Services.EventManager.Register<TimeOut>(OnGoalScored);
        
        Services.Players = new[] {new PlayerControlled(player1,10f)};
        Services.AIManager = new AIController();
        Services.AIManager.Initialize();

        Services.Input = new InputManager();


    }

    void Awake()
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

    public void OnGoalScored(AGPEvent e)
    {
        ball.transform.position = new Vector3();
    }    
    // public void OnTimeOut(AGPEvent e)
    // {
    //     ball.transform.position = new Vector3();
    // }

    public void Restart()
    {
        ball.transform.position = startingZone;
    }
}