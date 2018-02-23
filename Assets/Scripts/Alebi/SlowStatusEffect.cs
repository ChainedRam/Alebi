using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Player;
using UnityEngine;

public class SlowStatusEffect : StatusEffect
{
    private float SlowRate;

    public SlowStatusEffect(float slow) : base("Slow")
    {
        SlowRate = slow;
    }

    public override void Apply(Player p)
    {
       
    }

    public override void Init(Player p)
    {
        p.Speed *= SlowRate;
    }

    public override void Revert(Player p)
    {
        p.Speed *= (1/SlowRate); 
    }
}
