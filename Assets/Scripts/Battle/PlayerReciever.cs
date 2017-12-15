using ChainedRam.Alebi.Core;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainedRam.Core.Interface;

public class PlayerReciever : Collider2DReciever<PlayerPickable>
{

    public override void OnRecieve(PlayerPickable picked)
    {
        Projectile proj = Instantiate(picked.ProjectilePrefab, transform.position, transform.rotation, transform);

        proj.Setup(1, 90); 
    }

}
