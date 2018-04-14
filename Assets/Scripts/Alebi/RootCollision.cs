using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Player;
using UnityEngine;

public class RootCollision : PlayerCollisionEffect
{
    public float duration;
    private static StatusEffect rootEffect = new RootStatusEffect();

    public override void OnHit(Player p)
    {
        gameObject.SetActive(false);
        p.AddEffect(rootEffect, duration);
        p.velocityReset();
    }
}
