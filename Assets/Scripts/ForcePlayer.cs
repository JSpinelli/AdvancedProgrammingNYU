using UnityEngine;

public class ForcePlayer : Player
{
    Rigidbody2D rigidBody2D;
    public ForcePlayer(GameObject pl, float speed, GameObject ball):base(pl, speed, ball){
        rigidBody2D = pl.GetComponent<Rigidbody2D>();
    }
    public override void Move(){

        Vector3 force = Vector3.Normalize(ball.transform.position - playerAvatar.transform.position);
        Debug.Log(force);
        rigidBody2D.AddForce(force*10);
    }
}
