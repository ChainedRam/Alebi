using ChainedRam.Core;
using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilBuilder : OnceGenerator
{
    public TimedEvent OilBreak;

    public ProjectileGenerator OilSpawner;

    private Direction[] Directions = { Direction.NorthEast, Direction.SouthEast, Direction.SouthWest, Direction.NorthWest };

    protected override void OnGenerate(GenerateEventArgs e)
    {
        
    }
}
