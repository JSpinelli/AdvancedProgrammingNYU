using UnityEngine;
using BehaviourTree;

public class DefensivePlayer : Player
{
    private Tree<DefensivePlayer> tree;
    Rigidbody2D rigidBody2D;

    public DefensivePlayer(GameObject pl, float speed, Ability ability) : base(pl, speed,new []{ability})
    {
        rigidBody2D = pl.GetComponent<Rigidbody2D>();
        
        Node<DefensivePlayer>[] firstSequence =
        {
            new Condition<DefensivePlayer>(CloseToBall), 
            new Do<DefensivePlayer>(MoveToBall)
        };
        Node<DefensivePlayer>[] secondSequence =
        {
            new Not<DefensivePlayer>(new Condition<DefensivePlayer>(CloseToBall)), 
            new Do<DefensivePlayer>(StayPut)
        };
        Node<DefensivePlayer>[] mainSequence = {new Sequence<DefensivePlayer>(firstSequence), new Sequence<DefensivePlayer>(secondSequence)};
        tree = new Tree<DefensivePlayer>(new Selector<DefensivePlayer>(mainSequence));
    }

    private static bool CloseToBall(DefensivePlayer me)
    {
        return Vector3.Distance(me._gameObject.transform.position, me.ball.transform.position) < 5f;
    }

    private static bool MoveToBall(DefensivePlayer me)
    {
        Vector3 force = Vector3.Normalize(me.ball.transform.position - me._gameObject.transform.position);
        me.rigidBody2D.AddForce(force * Services.gameManager.movementSpeed);
        return true;
    }    
    private static bool StayPut(DefensivePlayer me)
    {
        me._gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f,0f,0f));
        me.rigidBody2D.velocity = Vector3.zero;
        me.rigidBody2D.angularVelocity = 0f;
        return true;
    }

    public override void Behaviours()
    {
        tree.Update(this);
        foreach (var ability in playerAbilities)
        {
            if (ability.ShouldTrigger(this))
            {
                ability.TriggerAbility(this);
            }
            ability.UpdateAbility(this);
        }
    }
}