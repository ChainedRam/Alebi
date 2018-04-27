using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Player;
using UnityEngine;


public class DamageCollision : PlayerCollisionEffect
{
    public int Damage; 

    public override void OnHit(Player p)
    {
        gameObject.SetActive(false);
        p.Health -= Damage;
    }
}