using ChainedRam.Core;
using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetSideCycleBuilder : Generator
{
    public ShowupOnScreenGenerator Enter;
    public ShowupOnScreenGenerator Exit;

    public PoolGenerator BothHandCoinSpawnerRepeater;

    public ProjectileGenerator RightCoinSpawner;
    public ProjectileGenerator LeftCoinSpawner;

    public MultiProjectileGenerator NetSpawner;

    public DelayedMotion RightCoinDelay;
    public DelayedMotion LeftCoinDelay;

    public int Rank;


    private Direction[] RankEnterDirection = { Direction.East, Direction.West };

    protected override void OnGenerate(GenerateEventArgs e)
    {
        Rank++;

        //Switch betweeb East (even) and West (odd)
        Enter.PositionProvider.SetToPosition(RankEnterDirection[Rank % 2], Vector2.right, PositionRelativeTo.Inside);
        Exit.PositionProvider.SetToPosition(RankEnterDirection[Rank % 2], Vector2.right, PositionRelativeTo.Outside);

        RightCoinDelay.LookAt.SetRotationOffset(Vector2.up * .25f * (1 - (2 * (Rank % 2) ) ) );
        LeftCoinDelay.LookAt.SetRotationOffset(Vector2.up * -.25f * (1 - (2 * (Rank % 2) ) ) );


        switch (Rank)
        {
            case 2:
                NetSpawner.Count *= 2;
                NetSpawner.FireRate /= 2;
                break;

            case 4:
                NetSpawner.Count *= 2;
                NetSpawner.FireRate /= 2; 

                BothHandCoinSpawnerRepeater.Repeat = 2;
                RightCoinSpawner.WaitTime /= 2;
                LeftCoinSpawner.WaitTime /= 2;
                break;
        }
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
