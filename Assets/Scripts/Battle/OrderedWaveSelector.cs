using ChainedRam.Alebi.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedWaveSelector : Selector
{
    public int StartingIndex;

    public int CurrentIndex;


    private void Start()
    {
        CurrentIndex = StartingIndex; 
    }

    public override T Select<T>(T[] waves, T previousWave = null)
    {
        return waves[(CurrentIndex++) % waves.Length]; 
    }
}
