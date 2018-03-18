using ChainedRam.Core;
using ChainedRam.Core.Configuration;
using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowupOnScreenConfig : ConfigMapper<ShowupOnScreenGenerator>
{
    public PositionProvider PositionProvider; 

    public override void Configure(ShowupOnScreenGenerator obj)
    {
        obj.PositionProvider = PositionProvider; 
    }
}
