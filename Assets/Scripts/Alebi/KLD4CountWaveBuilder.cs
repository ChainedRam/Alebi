using ChainedRam.Core;
using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KLD4CountWaveBuilder : Generator
{
    public ShowupOnScreenGenerator Enter;
    public ShowupOnScreenGenerator Exit;

    private Direction[] Directions = 
    {
        Direction.East,
        Direction.SouthEast,
        Direction.South,
        Direction.SouthWest,
        Direction.West,
        Direction.NorthWest,
        Direction.North,
        Direction.NorthEast,
    }; 

    public int Rank;

    protected override void Awake()
    {
        Rank = 0; 
    }

    protected override void OnGenerate(GenerateEventArgs e)
    {
        Direction selectedDirection = Directions[Rank++];

        Vector2 offset = Vector2.zero; 

        switch (selectedDirection)
        {
            case Direction.East:
            case Direction.West:
                offset = Vector2.right;
                break;

            case Direction.South:
            case Direction.North:
                offset = Vector2.up;
                break;

            case Direction.SouthWest:
            case Direction.NorthWest:
            case Direction.NorthEast:
            case Direction.SouthEast:
                offset = Vector2.one;
                break;
        }

        Enter.PositionProvider.SetToPosition(selectedDirection, offset, PositionRelativeTo.Inside);
        Exit.PositionProvider.SetToPosition(selectedDirection, offset, PositionRelativeTo.Outside);

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
