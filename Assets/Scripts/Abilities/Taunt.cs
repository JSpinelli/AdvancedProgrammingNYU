using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Abilities/Taunt")]
public class Taunt : Ability
{
    public override bool ShouldTrigger(Player p)
    {
        return Input.GetKeyDown(KeyCode.T);
    }

    public override void TriggerAbility(Player p)
    {
        p.playerAudioSource.clip = (AudioClip) Resources.Load("Sounds/taunt");
        p.playerAudioSource.Play();
    }

    public override void Update()
    {
        
    }
}
