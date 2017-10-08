using System.Collections;
using System.Collections.Generic;
using ChainedRam.Alebi.Battle;
using UnityEngine;

/// <summary>
/// Termenator that terminates based on number of projected projectiles. 
/// </summary>
public class CountWaveTerminator : WaveTerminator
{

    public int MaxGeneratedProjectiles;

    public int Count; 


    public override bool ShouldTerminate()
    {
        return Count >= MaxGeneratedProjectiles; 
    }

    public override void Setup(Pattern[] Patterns)
    {
        base.Setup(Patterns); 
        Count = 0; 
    }

    public override void OnProjectileLaunched(Pattern launcher, Projectile proj)
    {
        base.OnProjectileLaunched(launcher, proj);

        if (IsTerminating == false)
        {
            Count++;
        }
    }
}
