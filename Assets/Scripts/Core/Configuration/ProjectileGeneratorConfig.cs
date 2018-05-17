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
            var pooler = new ProjectilePooler(proj)
            {
                Capacity = 10 
            };
            Debug.LogWarning("[Debug] Capacity = 10");
            Poolers.Add(proj, pooler); 

        }

        return Poolers[proj]; 
    }

}
