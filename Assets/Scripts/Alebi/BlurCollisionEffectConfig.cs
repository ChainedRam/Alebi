using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainedRam.Core.Configuration;

public class BlurCollisionEffectConfig : ConfigMapper<BlurCollisionEffect>
{
    public BlurScreenEffect BlurScreenEffect; 

    public override void Configure(BlurCollisionEffect obj)
    {
        obj.ScreenEffect = BlurScreenEffect; 
    }
}
