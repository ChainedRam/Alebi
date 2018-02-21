using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Player;
using UnityEngine;

public class RootStatusEffect : StatusEffect
{
    public RootStatusEffect() : base("Root")
    {

    }

    public override void Apply(Player p)
    {
        p.Speed = 0;
    }
}
