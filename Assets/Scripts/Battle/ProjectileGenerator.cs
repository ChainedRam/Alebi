using ChainedRam.Alebi.Battle;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGenerator : QueueInstanceGenerator<Projectile>
{
    public float Velocity;

    [Range(-180,180)]
    public float Direction;

    public override void SetupGenerated(Projectile generated)
    {
        base.SetupGenerated(generated);

        generated.Setup(Velocity, Direction); 
    }

    public override bool ShouldGenerate()
    {
        return true; 
    }

    protected override void SkippedGeneration()
    {
        
    }
}
