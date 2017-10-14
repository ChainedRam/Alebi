﻿using ChainedRam.Alebi.Battle;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileQueueGenerator : QueueInstanceGenerator<Projectile>
{
    [Header("ProjectileGenerator Settings")]
    [Range(0, 25f)]
    public float RelaodDelay;

    [Tooltip("Velocity of generated projectile.")]
    public float Velocity;

    [Tooltip("Degree of generated projectile.")]
    [Range(-180, 180)]
    public float Angle;

    private float CurrentTime; 

    public override void SetupGenerated(Projectile pro)
    {
        pro.Setup(Velocity, Angle);
    }

    public override bool ShouldGenerate()
    {
        return (CurrentTime -= Time.deltaTime) < 0;
    }

    public abstract void Project(Projectile pro); 

    private void Start()
    {
        CurrentTime = RelaodDelay; 
    }
}