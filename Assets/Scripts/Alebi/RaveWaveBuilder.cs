using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaveWaveBuilder : Generator
{
    public PoolGenerator WalkSpawnRepeater;
    public DelayedMotion DaggerMotion;

    public NestedGenerator SandCycle; 
    public RotatingGenerator SpinningSand;
    public ProjectileGenerator SandSpawner;


    protected override void OnGenerate(GenerateEventArgs e)
    {
        WalkSpawnRepeater.Repeat *= 2;
        DaggerMotion.Delay += 5;

        SandCycle.WaitTime += 5;
        SpinningSand.WaitTime += 5; 
        SpinningSand.Loops -= 2;
        SandSpawner.WaitTime /= 2; 
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
