using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Abilities/Change Color")]
public class ChangeColorOnHit : Ability
{
    private Color originalColor;
    private float timer = 0;
    public float duration;
    public Color colorToChangeTo;
    private bool effectTriggered = false;
    private bool gotHit = false;
    private Player p;

    public override void SetUpAbility(Player p)
    {
        this.p = p;
        originalColor = p.playerRenderer.color;
        Services.EventManager.Register<PlayerCollision>(OnCollision);
    }
    
    private void OnCollision(AGPEvent e)
    {
        p.playerRenderer.color = colorToChangeTo;
        effectTriggered = true;
    }

    public override bool ShouldTrigger(Player p)
    {
        return false;
    }

    public override void TriggerAbility(Player p)
    {
    }

    public override void UpdateAbility(Player p)
    {
        if (effectTriggered)
        {
            if (timer >= duration)
            {
                effectTriggered = false;
                timer = 0;
                p.playerRenderer.color = originalColor;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}
