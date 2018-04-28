using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SandWaveMainCycleBuilder : OnceGenerator
{
    public PoolGenerator WalkSpawnRepeater;

    public DelayedMotion DaggerMotion;
    public DaggerDelayBuild DaggerBuild;

    public NestedGenerator SandCycle; 
    public RotatingGenerator SpinningSand;
    public ProjectileGenerator SandSpawner;

    protected override void OnGenerate(GenerateEventArgs e)
    {
        WalkSpawnRepeater.Repeat *= 2;
        DaggerMotion.Delay += 2.5f;
        DaggerBuild.Rank = 0; 

        SandCycle.WaitTime += 2f; 

        SpinningSand.Loops *= 4;
        SandSpawner.WaitTime /= 2; 
    }

}
