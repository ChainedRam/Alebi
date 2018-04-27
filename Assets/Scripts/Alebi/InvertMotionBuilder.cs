using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertMotionBuilder : OnceGenerator
{
    public TargetedMotion WalkAroundMotion;
    public DaggerDelayBuild DaggerBuild; 

    protected override void OnGenerate(GenerateEventArgs e)
    {
        WalkAroundMotion.Invert = !WalkAroundMotion.Invert;

        DaggerBuild.Rank = 0; 
    }
}
