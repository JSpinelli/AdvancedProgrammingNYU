using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefereeBehaviour
{
    public GameObject refereeBody;
    public float distanceFromBall;
    public GameObject ball;
    private FiniteStateMachine<RefereeBehaviour> _fsm;

    public float lastFoulSeverity;

    public RefereeBehaviour(GameObject body, float distance)
    {
        refereeBody = body;
        ball = Services.gameManager.ball;
        distanceFromBall = distance;
        _fsm = new FiniteStateMachine<RefereeBehaviour>(this);
        _fsm.TransitionTo<Waiting>();
    }

    public void Update()
    {
        _fsm.Update();
    }

    #region States
    public abstract class RefereeState : FiniteStateMachine<RefereeBehaviour>.State
    {
    }

    public class Follow : RefereeState
    {
        private Transform ball;
        private Transform referee;
        public override void OnEnter()
        {
            ball = Context.ball.transform;
            referee = Context.refereeBody.transform;
            Services.EventManager.Register<PlayerCollision>(OnPlayerCollision);
        }

        public override void Update()
        {
            if (Vector3.Distance(referee.position, ball.position) > (Context.distanceFromBall +0.1f))
            {
                referee.position = UnClampLerp(referee.position, ball.position, Time.deltaTime);
            }
            else if (Vector3.Distance(referee.position, ball.position) < (Context.distanceFromBall -0.1f))
            {
                referee.position = UnClampLerp(referee.position, ball.position, -Time.deltaTime);
            }
        }

        private void OnPlayerCollision(AGPEvent e)
        {
            PlayerCollision pc = (PlayerCollision) e;
            if (pc.severity >= Services.gameManager.foulSeverity)
            {
                Context.lastFoulSeverity = pc.severity;
                TransitionTo<ShowCard>();
            }
        }
        
        public Vector3 UnClampLerp( Vector3 a, Vector3 b, float t ){
            return a + (b - a) * t;
        }

        public override void OnExit()
        {
            Services.EventManager.Unregister<PlayerCollision>(OnPlayerCollision);
        }
    }

    public class ShowCard : RefereeState
    {

        private float cardTimer;
        private GameObject activeCard;
        public override void OnEnter()
        {
            cardTimer = 2f;
            if (Context.lastFoulSeverity * 2 > Services.gameManager.foulSeverity)
            {
                activeCard = Context.refereeBody.transform.GetChild(0).gameObject;
                activeCard.SetActive(true);
            }
            else
            {
                activeCard = Context.refereeBody.transform.GetChild(1).gameObject;
                activeCard.SetActive(true);
            }
        }

        public override void Update()
        {
            if (cardTimer > 0)
            {
                cardTimer -= Time.deltaTime;
            }
            else
            {
                TransitionTo<Follow>();
            }
        }
        public override void OnExit()
        {
            activeCard.SetActive(false);
        }

    }

    public class ScoreGoal : RefereeState
    {
        public override void Update()
        {
        }
    }    
    
    private class Waiting : RefereeState
    {
        
        public override void OnEnter()
        {
            Services.EventManager.Register<GameStart>(OnGameStart);
        }

        private void OnGameStart(AGPEvent e)
        {
            TransitionTo<Follow>();
        }

        public override void OnExit()
        {
            Services.EventManager.Unregister<GameStart>(OnGameStart);
        }
    }

    #endregion
}
