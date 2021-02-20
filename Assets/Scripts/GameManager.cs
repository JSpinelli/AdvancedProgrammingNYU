using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject ball;
    public GameObject referee;

    public float movementSpeed = 0.01f;
    public float playerSpeed = 10f;

    public float foulSeverity = 5f;
    
    public float refereeDistance = 1f;

    public float durationOfMatch = 10f;

    private Vector3 startingZone;

    public FiniteStateMachine<GameManager> _fsm; 

    void _InitializeServices()
    {
        Services.gameManager = this;
        Services.EventManager = new EventManager();
        Services.EventManager.Register<GoalScored>(OnGoalScored);
        Services.EventManager.Register<GameEnd>(OnGoalScored);

        Services.Players = new[] {new PlayerControlled(player1, playerSpeed)};
        Services.AIManager = new AIController();
        Services.AIManager.Initialize();

        Services.Input = new InputManager();

        Services.RefereeBehaviour = new RefereeBehaviour(referee,refereeDistance);
    }

    void Awake()
    {
        startingZone = ball.transform.position;
        _InitializeServices();
        _fsm = new FiniteStateMachine<GameManager>(this);
        _fsm.TransitionTo<Menu>();
    }

    // Update is called once per frame
    private void Update()
    {
        _fsm.Update();
        Services.RefereeBehaviour.Update();
    }

    public bool IsPlaying()
    {
        if (_fsm.CurrentState == null) return false;
        return _fsm.CurrentState.GetType() == typeof(GamePlaying);
    }

    private void OnDestroy()
    {
        Services.AIManager.Destroy();
    }

    public void OnGoalScored(AGPEvent e)
    {
        ball.transform.position = new Vector3();
        ball.transform.rotation = Quaternion.Euler(new Vector3(0f,0f,0f));
        ball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }

    public void Restart()
    {
        ball.transform.position = startingZone;
    }

    #region States

    public abstract class GameState : FiniteStateMachine<GameManager>.State
    {
    }

    public class Menu : GameState
    {
        public override void OnEnter()
        {
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TransitionTo<GamePlaying>();
                Services.EventManager.Fire(new GameStart());
            }
        }

        public override void OnExit()
        {
            
        }
    }

    public class GamePlaying : GameState
    {
        public override void OnEnter()
        {
            Services.EventManager.Register<GameEnd>(OnTimeOut);
        }

        public override void Update()
        {
            Services.Players[0].Update();
            Services.AIManager.Update();
        }

        public void OnTimeOut(AGPEvent e)
        {
            TransitionTo<TitleScreen>();
        }
        
        public override void OnExit()
        {
            Services.EventManager.Unregister<GameEnd>(OnTimeOut);
        }

    }

    public class TitleScreen : GameState
    {
        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TransitionTo<GamePlaying>();
                Services.EventManager.Fire(new GameStart());
            }
        }
    }

    #endregion
}