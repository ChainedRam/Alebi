using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Player;
using UnityEngine;

public class SlowStatusEffect : StatusEffect
{
    private float Slow;

    public SlowStatusEffect(float duration, float slow) : base("Slow")
    {
        Slow = slow;
    }

    public override void Apply(Player p)
    {
        p.Speed *= Slow; 
    }
}
