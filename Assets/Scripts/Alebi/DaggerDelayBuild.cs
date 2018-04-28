using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerDelayBuild : OnceGenerator
{
    public DelayedMotion DaggerDelay;


    public int Rank; 

    protected override void OnGenerate(GenerateEventArgs e)
    {
        Rank++;

        DaggerDelay.Delay = 7 + ((float)Rank)/4; 
    }
}
