using ChainedRam.Alebi.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Generator
{

    public Phase[] Phases;

    private int CurrentPhaseIndex; //Big SHAQ

    protected override bool ShouldGenerate()
    {
        return !Phases[CurrentPhaseIndex].IsGenerating; 
    }

}
