using System.Collections.Generic;
using UnityEngine;

public class PlayerControlled : Player
{
    Rigidbody2D rigidBody2D;
    public PlayerControlled(Ability[] abilities,GameObject pl, float speed) : base(pl, speed, abilities)
    {
        rigidBody2D = pl.GetComponent<Rigidbody2D>();
    }
    public override void Update()
    {

        if (Input.GetAxis("Vertical") != 0)
        {
            rigidBody2D.AddForce(new Vector2(0, Input.GetAxis("Vertical") * speed));
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            rigidBody2D.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0));
        }

        foreach (var ability in playerAbilities)
        {
            if (ability.ShouldTrigger(this))
            {
                ability.TriggerAbility(this);
            }
            ability.UpdateAbility(this);
        }
    }

    //Using InputManager
    // public override void Update()
    // {
    //     if (Services.Input.KeysDown.Contains(KeyCode.W))
    //     {
    //         rigidBody2D.AddForce(new Vector2(0, speed));
    //     }
    //
    //     if (Services.Input.KeysDown.Contains(KeyCode.S))
    //     {
    //         rigidBody2D.AddForce(new Vector2(0, -speed));
    //     }
    //
    //     if (Services.Input.KeysDown.Contains(KeyCode.A))
    //     {
    //         rigidBody2D.AddForce(new Vector2(-speed, 0));
    //     }
    //
    //     if (Services.Input.KeysDown.Contains(KeyCode.D))
    //     {
    //         rigidBody2D.AddForce(new Vector2(speed, 0));
    //     }
    // }
}
