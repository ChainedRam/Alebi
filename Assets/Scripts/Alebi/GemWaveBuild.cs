using ChainedRam.Core;
using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemWaveBuild : Generator
{

    public ShowupOnScreenGenerator Enter;
    public ShowupOnScreenGenerator Exit;

    public NestedGenerator Spawner; 

    public ProjectileGenerator GemSpawner;
    public ProjectileGenerator DaggerSpawner;

    private Direction[] Directions = { Direction.NorthEast, Direction.SouthWest, Direction.SouthEast, Direction.NorthWest }; 

    public int Rank = 0;

    protected override void OnGenerate(GenerateEventArgs e)
    {
        float waitTime = Mathf.Max(.5f, (4 - (Rank / 4)));

        Enter.WaitTime = Mathf.Min(waitTime / 2, .5f);
        Spawner.WaitTime = Mathf.Min(waitTime / 2, .5f);
        Exit.WaitTime = Mathf.Min(waitTime, 1f);

        Direction selected = Directions[(Rank++)% Directions.Length];

        Enter.PositionProvider.SetToPosition(selected, Vector2.one, PositionRelativeTo.Inside);
        Exit.PositionProvider.SetToPosition(selected, Vector2.one, PositionRelativeTo.Outside);

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
