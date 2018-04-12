using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainedRam.Core.Configuration;
using ChainedRam.Core.Generation;

public class GeneratorConfig : ConfigMapper<Generator>
{
    public bool DisableOnStart; 

    public override void Configure(Generator obj)
    {
        if(DisableOnStart)
        {
            obj.enabled = false; 
        }
    }
}
