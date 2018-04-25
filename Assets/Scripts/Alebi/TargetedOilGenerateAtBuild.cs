using ChainedRam.Core;
using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedOilGenerateAtBuild : OnceGenerator
{
    public ProjectileGenerator OilSpawner;

    private Direction[] Directions = { Direction.NorthEast, Direction.NorthWest, Direction.SouthEast, Direction.SouthWest };
    private int Rank;

    protected override void OnGenerate(GenerateEventArgs e)
    {
        Rank++;

        var direction = Directions[Rank % Directions.Length];

        OilSpawner.GenerateAt.SetToPosition(direction, Vector2.one, PositionRelativeTo.Inside);

    }
}
