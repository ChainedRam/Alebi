using ChainedRam.Core.Configuration;
using ChainedRam.Core.Generation;
using ChainedRam.Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargettedConfigMapper : ConfigMapper<PlayerTarggettedProjectileGenerator>
{
    public Player Player; 

    public override void Configure(PlayerTarggettedProjectileGenerator obj)
    {
        obj.Target.SetToTransform(Player.gameObject.transform); 
    }

}
