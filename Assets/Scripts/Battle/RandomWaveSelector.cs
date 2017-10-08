using ChainedRam.Alebi.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// selects a random wave 
/// </summary>
public class RandomWaveSelector : WaveSelector
{
    public bool AllowConsecutiveRepetition; 

    public override Wave SelectWave(Wave[] waves, Wave previousWave)
    {
        Wave selected;

        do
        {
            selected = waves[Random.Range(0, waves.Length)]; 
        } while (AllowConsecutiveRepetition && previousWave != null && selected == previousWave); 

        return selected; 
    }
}
