using ChainedRam.Core.Configuration;
using ChainedRam.Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargettedConfigMapper : ConfigMapper<PlayerTarggettedProjectileGeneration>
{
    public Player Player; 

    public override void Configure(PlayerTarggettedProjectileGeneration obj)
    {
        obj.Target = Player.gameObject; 
    }

}
