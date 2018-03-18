using ChainedRam.Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurCollisionEffect : CollisionEffect
{
    public string Name { get;set; }

    public BlurScreenEffect ScreenEffect;

    public float Duration;

    public override void OnHit(Player p)
    {
        p.AddEffect(ScreenEffect, Duration);

        gameObject.SetActive(false); 
    }
}
