using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertMotionBuilder : OnceGenerator
{
    public TargetedMotion WalkAroundMotion;

    protected override void OnGenerate(GenerateEventArgs e)
    {
        WalkAroundMotion.Invert = !WalkAroundMotion.Invert;
    }
}
