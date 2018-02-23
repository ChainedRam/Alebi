using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Player;
using UnityEngine;

public class RootStatusEffect : StatusEffect
{
    public RootStatusEffect() : base("Root")
    {

    }

    public override void Init(Player p)
    {
        p.velocityReset(); 
    }

    public override void Apply(Player p)
    {
        p.Speed = 0;
    }

    public override void Revert(Player p)
    {
       
    }
}
