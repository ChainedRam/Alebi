using System.Collections;
using System.Collections.Generic;
using ChainedRam.Alebi.Battle;
using UnityEngine;

/// <summary>
/// Time based wave terminator 
/// </summary>
public class TimedWaveTerminator : WaveTerminator {


    [Tooltip("Time to wait in seconds.")]
    [Range(0,1000)]
    public float WaitTime;

    private float CurrentTimer; 
  
	// Update is called once per frame
	void Update ()
    {
        CurrentTimer -= Time.deltaTime; 
    }

    public override bool ShouldTerminate()
    {
        return CurrentTimer <= 0; 
    }

    public override void Setup(Pattern[] Patterns)
    {
        base.Setup(Patterns); 

        CurrentTimer = WaitTime;
    }
}
