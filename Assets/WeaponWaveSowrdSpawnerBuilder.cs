using ChainedRam.Core;
using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWaveSowrdSpawnerBuilder : OnceGenerator
{

    public ProjectileGenerator SwordGenerator;

    public LineMotion SwordMotion;


    private Direction[] Directions = { Direction.NorthEast, Direction.NorthWest, Direction.SouthEast, Direction.SouthWest};
    private float[] Angles = { -90, 0, 90, 180 };

    protected override void OnGenerate(GenerateEventArgs e)
    {
       
    }
}
