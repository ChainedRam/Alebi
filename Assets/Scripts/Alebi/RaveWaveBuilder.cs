using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class RaveWaveBuilder : OnceGenerator
{
    public PoolGenerator WalkSpawnRepeater;

    public DelayedMotion DaggerMotion; 

    public NestedGenerator SandCycle; 
    public RotatingGenerator SpinningSand;
    public ProjectileGenerator SandSpawner;

    protected override void OnGenerate(GenerateEventArgs e)
    {
        WalkSpawnRepeater.Repeat *= 2;

        DaggerMotion.Delay += 2.5f;

        SandCycle.WaitTime = 12f; 

        SpinningSand.Loops *= 4;
        SandSpawner.WaitTime /= 2; 
    }

}
