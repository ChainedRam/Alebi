using ChainedRam.Core.Configuration;
using ChainedRam.Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingPlayerConfig : ConfigMapper<PlayerTracking>
{
    public Player Player; 

    public override void Configure(PlayerTracking obj)
    {
        obj.Target = Player.gameObject; 
    }


}
