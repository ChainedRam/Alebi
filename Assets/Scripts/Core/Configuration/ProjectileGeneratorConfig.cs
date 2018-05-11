using ChainedRam.Core.Configuration;
using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ProjectileGeneratorConfig : ConfigMapper<ProjectileGenerator>
{
    private Dictionary<Projectile, ProjectilePooler> Poolers;

    public override void Prepare()
    {
       Poolers = new Dictionary<Projectile, ProjectilePooler>();
    }

    public override void Configure(ProjectileGenerator projectileGenerator)
    {
        projectileGenerator.InstancePooler = Register(projectileGenerator.Prefab); 
    }

    private ProjectilePooler Register(Projectile proj)
    {
        if(!Poolers.ContainsKey(proj))
        {
            Poolers.Add(proj, new ProjectilePooler(proj)); 
        }

        return Poolers[proj]; 
    }

}
