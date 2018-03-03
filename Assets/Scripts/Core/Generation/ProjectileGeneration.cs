using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGeneration : WaitGeneration
{
    public Projectile ProjectilePrefab;

    public Transform GenerateAt;  

    public ChainedRam.Core.Projection.Motion Motion; 

    public override void Trigger(Generator sender)
    {
        ProjectilePrefab.gameObject.SetActive(false);

        Projectile proj = Instantiate(ProjectilePrefab, GenerateAt, false);
        proj.Motion = Motion;

        proj.Setup(1);
        proj.gameObject.SetActive(true); 
    }
}
