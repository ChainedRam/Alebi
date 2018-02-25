using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Player;
using UnityEngine;
using ChainedRam.Core.Projection;

public class OilCollision : CollisionEffect {

    public GameObject oil;
    public SpriteRenderer bottleSprite;
    public Projectile bottle;

    public override void OnHit(Player p)
    {
        BreakBottle();
    }

    public void BreakBottle()
    {
        bottle.enabled = false;
        bottleSprite.enabled = false;
        oil.transform.position = bottle.transform.position;
        oil.SetActive(true);
    }
}
