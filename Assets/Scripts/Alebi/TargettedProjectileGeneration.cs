using System;
using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using UnityEngine;

public class TargettedProjectileGeneration : ProjectileGeneration
{
    [Header("Targetted Options")]
    public GameObject Target; 

    public float PrepTime;

    private bool IsMoving;

    private Projectile Projectile; 

    public override void Trigger(Generator sender)
    {
        base.Trigger(sender); 
        StartCoroutine(StareAtPlayer(PrepTime, sender));
    }

    private void Update()
    {
        if (IsMoving == true)
        {
            Projectile.transform.up = (Target.transform.position - Projectile.transform.position);
        }
    }

    private IEnumerator StareAtPlayer(float prepTime, Generator g)
    {
        IsMoving = true; 
        yield return new WaitForSeconds(prepTime);

        
        IsMoving = false;  
    }

    protected override Projectile CreateProjectile()
    {
        Projectile = base.CreateProjectile();

        DelayedMotion delayedMotion = Projectile.gameObject.AddComponent<DelayedMotion>();
        delayedMotion.Delay = PrepTime;
        delayedMotion.Warpped = Projectile.Motion;

        Projectile.Motion = delayedMotion;
        Projectile.Setup(1); 


        return Projectile; 
    }
}


