using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedGemGenerator : ProjectileGenerator {

    public float MaxRandom;
    public float ExplosionTimer;
    [HideInInspector]
    public int ShardCount = 5;

    public override void SetupGenerated(Projectile generated)
    {
        base.SetupGenerated(generated);
        var timedEventComponent = generated.GetComponent<TimedEvent>();
        if (timedEventComponent != null)
        {
            timedEventComponent.time = ExplosionTimer + Random.Range(0,MaxRandom);
        }
        var shatterCollision = generated.GetComponent<ShatterCollision>();
        shatterCollision.ShardCount = ShardCount;
    }
}
