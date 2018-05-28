using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : InstancePooler<Projectile>
{
    private Projectile Prefab;
    private int i = 0; 

    public ProjectilePooler(Projectile prefab, int capacity = DefaultCapacity) : base()
    {
        Prefab = prefab; 
    }

    protected override Projectile CreateInstance()
    {
        var x =  Object.Instantiate(Prefab);

        x.name += " "+i++;

        return x; 
    }

    protected override void OnReused(Projectile instance)
    {
        instance.gameObject.SetActive(false); 
    }
}
