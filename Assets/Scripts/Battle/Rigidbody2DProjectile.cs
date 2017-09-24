using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Battle
{
    public class Rigidbody2DProjectile : Projectile
    {
        public Rigidbody2D body;

        public override void SetDirection(float degrees)
        {
            body.rotation = degrees;
        }

        public override void SetVelocity(Vector2 velocity)
        {
            body.velocity = velocity;
        }
    }
}
