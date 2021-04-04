using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public abstract bool ShouldTrigger(Player p);

    public abstract void TriggerAbility(Player p);

    public abstract void Update();
}
