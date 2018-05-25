using ChainedRam.Core.Configuration;
using ChainedRam.Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ChainedRam.Core.Configuration
{
    public class PlayerEnjectorConfig : ConfigMapper<Enjection>
    {
        public Player.Player Player;

        public override void Configure(Enjection obj)
        {
            obj.Enject(Player);
        }
    } 
}
