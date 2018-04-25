using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWaveAttackCycleBuild : OnceGenerator
{
    public NestedGenerator AttackCycle; 
    public MultiProjectileGenerator SwordSpawner;
    public PoolGenerator OilRepeater;

    public MultiProjectileGenerator CoinGenrator; 

    private int Rank; 

    protected override void OnGenerate(GenerateEventArgs e)
    {
        Rank++;

        AttackCycle.WaitTime *= 2; 
        SwordSpawner.Count *= 2;
        SwordSpawner.WaitTime *= 2; 

        OilRepeater.Repeat *= 2;

        if (Rank == 2)
        {
            CoinGenrator.FireRate = 0.5f; 
        }
    }
}
