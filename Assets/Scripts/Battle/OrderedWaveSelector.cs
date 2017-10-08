using ChainedRam.Alebi.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedWaveSelector : WaveSelector
{
    public int StartingIndex;

    public int CurrentIndex;


    private void Start()
    {
        CurrentIndex = StartingIndex; 
    }

    public override Wave SelectWave(Wave[] waves, Wave previousWave)
    {
        return waves[(CurrentIndex++) % waves.Length]; 
    }
}
