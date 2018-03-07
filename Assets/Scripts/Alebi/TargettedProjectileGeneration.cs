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

    public float PrepTime;

    private bool IsMoving;

    private Projectile Projectile; 

    protected override void OnGenerate(GenerateEventArgs e)
    {
        base.OnGenerate(e);
        StartCoroutine(StareAtPlayer(PrepTime));
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

        DelayedMotion delayedMotion = Projectile.gameObject.AddComponent<DelayedMotion>();
        delayedMotion.Delay = PrepTime;
        delayedMotion.Warpped = Projectile.Motion;

        Projectile.Motion = delayedMotion;
        Projectile.Setup(1);
        Projectile.enabled = true;

        return Projectile; 
    }

    public override void SetupGenerated(Projectile generated)
    {
        Projectile = generated;
        generated.enabled = false;
        base.SetupGenerated(generated);

        DelayedMotion delayedMotion = Projectile.gameObject.AddComponent<DelayedMotion>();
        delayedMotion.Delay = PrepTime;
        delayedMotion.Warpped = Projectile.Motion;

        Projectile.Motion = delayedMotion;
        Projectile.Setup(1);
       

        generated.enabled = true; 


    }
}


