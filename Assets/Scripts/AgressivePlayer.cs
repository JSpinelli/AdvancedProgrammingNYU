using UnityEngine;
using BehaviourTree;

public class AgressivePlayer : Player
{
    
    //Need to Change
    private Tree<AgressivePlayer> tree;
    Rigidbody2D rigidBody2D;

    public AgressivePlayer(GameObject pl, float speed) : base(pl, speed)
    {
        rigidBody2D = pl.GetComponent<Rigidbody2D>();
        
        Node<AgressivePlayer>[] firstSequence =
        {
            new Condition<AgressivePlayer>(CloseToBall), 
            new Do<AgressivePlayer>(MoveToBall)
        };
        Node<AgressivePlayer>[] secondSequence =
        {
            new Not<AgressivePlayer>(new Condition<AgressivePlayer>(CloseToBall)), 
            new Do<AgressivePlayer>(StayPut)
        };
        Node<AgressivePlayer>[] mainSequence = {new Sequence<AgressivePlayer>(firstSequence), new Sequence<AgressivePlayer>(secondSequence)};
        tree = new Tree<AgressivePlayer>(new Selector<AgressivePlayer>(mainSequence));
    }

    private static bool CloseToBall(AgressivePlayer me)
    {
        return Vector3.Distance(me._gameObject.transform.position, me.ball.transform.position) < 5f;
    }

    private static bool MoveToBall(AgressivePlayer me)
    {
        Vector3 force = Vector3.Normalize(me.ball.transform.position - me._gameObject.transform.position);
        me.rigidBody2D.AddForce(force * Services.gameManager.movementSpeed);
        return true;
    }    
    private static bool StayPut(AgressivePlayer me)
    {
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