using UnityEngine;

public class PlayerControlled : Player
{
    Rigidbody2D rigidBody2D;
    public PlayerControlled(GameObject pl, float speed, GameObject ball) : base(pl, speed, ball)
    {
        rigidBody2D = pl.GetComponent<Rigidbody2D>();
    }
    public override void Move()
    {

        if (Input.GetAxis("Vertical") != 0)
        {
            rigidBody2D.AddForce(new Vector2(0, Input.GetAxis("Vertical") * speed));
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            rigidBody2D.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0));
        }
    }
}
