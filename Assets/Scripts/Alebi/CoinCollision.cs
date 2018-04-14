using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Player;
using UnityEngine;

public class CoinCollision : PlayerCollisionEffect
{
    public override void OnHit(Player p)
    {
        gameObject.SetActive(false);
        p.Health--;
    }
}