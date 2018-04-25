using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Player;
using UnityEngine;
using ChainedRam.Core.Projection;

public class OilBreakCollision : CollisionEffect<OilBreaker>
{
    public GameObject oil;
    public GameObject bottle; 
    public Projectile bottleProjectile;

    public override void OnHit(OilBreaker p)
    {
        print("break oil");
        oil.transform.position = p.transform.position;
        BreakBottle();
    }

    public void BreakBottle()
    {
        bottleProjectile.enabled = false;
        bottle.SetActive(false);
        oil.SetActive(true);
    }
}
