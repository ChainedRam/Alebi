using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAroundWaveBuilder : Generator
{
    public TargetedMotion WalkAroundMotion; 

    protected override void OnGenerate(GenerateEventArgs e)
    {
        WalkAroundMotion.Invert = !WalkAroundMotion.Invert; 


    }

    protected override bool ShouldGenerate()
    {
        return ShouldGenerateOnce(); 
    }

    protected override bool ShouldTerminate()
    {
        return ShouldTerminateOnSkippedGeneration(); 
    }

}
