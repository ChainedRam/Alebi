using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGeneration : WaitGeneration
{
    public Projectile ProjectilePrefab;

    public Transform GenerateAt;
    public Transform Parent; 

    public ChainedRam.Core.Projection.Motion Motion; 

    public override void Trigger(Generator sender)
    {
        ProjectilePrefab.gameObject.SetActive(false);

        Projectile proj = CreateProjectile();
        LaunchProjectile(proj); 
    }

    protected virtual Projectile CreateProjectile()
    {
        Projectile proj = Instantiate(ProjectilePrefab.gameObject, GenerateAt.position, GenerateAt.rotation).GetComponent<Projectile>();

        Motion.Initialize(proj, 1); 
        proj.Motion = Motion;
        return proj; 
    }

    protected virtual void LaunchProjectile(Projectile proj)
    {
        proj.Setup(1);
        proj.gameObject.SetActive(true);
    }

}
