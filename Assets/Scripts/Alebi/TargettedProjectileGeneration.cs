using System;
using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using UnityEngine;

public class TargettedProjectileGeneration : ProjectileGenerator
{
    [Header("Targetted Options")]
    public GameObject Target; 

    private bool IsMoving;

    private Projectile Projectile; 

    protected override void OnGenerate(GenerateEventArgs e)
    {
        base.OnGenerate(e);
        StartCoroutine(StareAtPlayer(WaitTime));
    }

    protected override void Update()
    {
        base.Update(); 
        if (IsMoving == true)
        {
            Projectile.transform.up = (Target.transform.position - Projectile.transform.position);
        }
    }

    private IEnumerator StareAtPlayer(float prepTime)
    {
        IsMoving = true; 
        yield return new WaitForSeconds(prepTime);

        
        IsMoving = false;  
    }

    protected override Projectile CreateInstance()
    {
        Projectile = base.CreateInstance();
        Projectile.enabled = false; 

        return Projectile; 
    }

    public override void SetupGenerated(Projectile generated)
    {
        base.SetupGenerated(generated);

        DelayedMotion delayedMotion = Projectile.gameObject.AddComponent<DelayedMotion>();
        delayedMotion.Delay = WaitTime;
        delayedMotion.Warpped = MotionOverride ?? Projectile.Motion;

        Projectile.Motion = delayedMotion;
        Projectile.enabled = true;
    }
}


