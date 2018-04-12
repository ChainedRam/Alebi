using ChainedRam.Core;
using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KLDWaveBuilder : Generator
{
    public ShowupOnScreenGenerator Enter;
    public ShowupOnScreenGenerator Exit;

    public PoolGenerator BothHandCoinSpawnerRepeater;

    public ProjectileGenerator RightCoinSpawner;
    public ProjectileGenerator LeftCoinSpawner;

    public PoolGenerator NetSpawnerRepeater;
    public ProjectileGenerator NetSpawner; 

    public int Rank;


    private Direction[] RankEnterDirection = { Direction.East, Direction.West };

    protected override void OnGenerate(GenerateEventArgs e)
    {
        Rank++;

        //Switch betweeb East (even) and West (odd)
        Enter.PositionProvider.SetToPosition(RankEnterDirection[Rank % 2], Vector2.right, PositionRelativeTo.Inside);
        Exit.PositionProvider.SetToPosition(RankEnterDirection[Rank % 2], Vector2.right, PositionRelativeTo.Outside);

        switch (Rank)
        {
            case 2:
                NetSpawnerRepeater.Repeat = 2;
                NetSpawner.WaitTime /= 2;
                break;

            case 4:
                NetSpawnerRepeater.Repeat = 4;
                NetSpawner.WaitTime /= 2; 

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
