using UnityEngine;

public class ForcePlayer : Player
{
    Rigidbody2D rigidBody2D;
    public ForcePlayer(GameObject pl, float speed):base(pl, speed){
        rigidBody2D = pl.GetComponent<Rigidbody2D>();
    }
    public override void Update(){

        Vector3 force = Vector3.Normalize(ball.transform.position - _gameObject.transform.position);
        rigidBody2D.AddForce(force*10);
    }
}
