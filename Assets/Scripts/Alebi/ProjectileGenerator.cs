using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileGenerator : QueueInstanceGenerator<Projectile>
{
    [Header("Projectile Generator")]
    public ChainedRam.Core.Projection.Motion Motion;

    private bool GenerateOnce; 

    public override void SetupGenerated(Projectile generated)
    {
        base.SetupGenerated(generated);
        generated.Motion = Motion; 
        generated.Setup(Delta);
    }

    protected override bool ShouldGenerate()
    {
        if (GenerateOnce)
        {
            GenerateOnce = false; 
            return true;
        }

        return false; 
    }

    protected override void Awake()
    {
        base.Awake();
        GenerateOnce = true; 
    }

    protected override void OnBegin()
    {
        base.OnBegin();
        GenerateOnce = true; 
    }
}

