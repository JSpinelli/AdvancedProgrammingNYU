using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Abilities/Taunt")]
public class Taunt : Ability
{
    public override void SetUpAbility(Player p)
    {
        
    }

    public override bool ShouldTrigger(Player p)
    {
        return Input.GetKeyDown(KeyCode.T);
    }

    public override void TriggerAbility(Player p)
    {
        Debug.Log("Triggered");
        //Services.audioSource.clip = (AudioClip) Resources.Load("Sounds/taunt");
        Services.audioSource.Play();
    }

    public override void UpdateAbility(Player p)
    {
        
    }
}
