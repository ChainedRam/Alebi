using ChainedRam.Core.Extentions;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rigidbody2DProjectile : Projectile
{
    public Rigidbody2D body;

    public override void Setup(float speed, float direction)
    {
        body.rotation = direction;
        body.velocity = (((Vector2)transform.parent.right) * speed).Rotate(body.rotation); 
    }
}

