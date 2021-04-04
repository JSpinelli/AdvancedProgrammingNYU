using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Freeze Ball")]
public class FreezeBall : Ability
{
    private GameObject ball;

    public float duration;
    public float cooldown;

    private float timer;
    private float cooldownTimer;

    private bool isActive = false;

    private bool onCooldown = false;

    // Update is called once per frame
    public override void SetUpAbility(Player p)
    {
        ball = Services.gameManager.ball;
    }

    public override bool ShouldTrigger(Player p)
    {
        return Input.GetKeyDown(KeyCode.Y) && !onCooldown;
    }

    public override void TriggerAbility(Player p)
    {
        isActive = true;
        onCooldown = true;
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
    }

    public override void UpdateAbility(Player p)
    {
        if (isActive)
        {
            if (timer >= duration)
            {
                isActive = false;
                timer = 0;
                ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

        if (onCooldown)
        {
            if (cooldownTimer >= cooldown)
            {
                cooldownTimer = 0;
                onCooldown = false;
            }
            else
            {
                cooldownTimer += Time.deltaTime;
            }
        }
    }
}