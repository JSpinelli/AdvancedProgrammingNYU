using System;
using UnityEngine;
using BehaviourTree;

public class BehaviourTreePlayer : Player
{
    private Tree<BehaviourTreePlayer> tree;
    Rigidbody2D rigidBody2D;

    public BehaviourTreePlayer(GameObject pl, float speed) : base(pl, speed)
    {
        rigidBody2D = pl.GetComponent<Rigidbody2D>();
        
        Node<BehaviourTreePlayer>[] firstSequence =
        {
            new Condition<BehaviourTreePlayer>(CloseToBall), 
            new Do<BehaviourTreePlayer>(MoveToBall)
        };
        Node<BehaviourTreePlayer>[] secondSequence =
        {
            new Not<BehaviourTreePlayer>(new Condition<BehaviourTreePlayer>(CloseToBall)), 
            new Do<BehaviourTreePlayer>(StayPut)
        };
        Node<BehaviourTreePlayer>[] mainSequence = {new Sequence<BehaviourTreePlayer>(firstSequence), new Sequence<BehaviourTreePlayer>(secondSequence)};
        tree = new Tree<BehaviourTreePlayer>(new Selector<BehaviourTreePlayer>(mainSequence));
    }

    private static bool CloseToBall(BehaviourTreePlayer me)
    {
        return Vector3.Distance(me._gameObject.transform.position, me.ball.transform.position) < 5f;
    }

    private static bool MoveToBall(BehaviourTreePlayer me)
    {
        Debug.Log("MOVE TO BALL");
        Vector3 force = Vector3.Normalize(me.ball.transform.position - me._gameObject.transform.position);
        me.rigidBody2D.AddForce(force * Services.gameManager.movementSpeed);
        return true;
    }    
    private static bool StayPut(BehaviourTreePlayer me)
    {
        Debug.Log("STAY PUT");
        me._gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f,0f,0f));
        me.rigidBody2D.velocity = Vector3.zero;
        me.rigidBody2D.angularVelocity = 0f;
        return true;
    }

    public override void Update()
    {
        tree.Update(this);
    }
}