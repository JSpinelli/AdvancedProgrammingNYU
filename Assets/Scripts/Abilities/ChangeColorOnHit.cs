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
    
    public override bool ShouldTrigger(Player p)
    {
        return p.gotHit;
    }

    public override void TriggerAbility(Player p)
    {
        originalColor = p.playerRenderer.color;
    }

    public override void Update()
    {
       
    }
}
