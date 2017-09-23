using ChainedRam.Alebi.Core;
using ChainedRam.Alebi.Interface.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Wave Runs a set of pattern under it. 
/// </summary>
//TODO abstract 
public class Wave : Runnable
{
    /// <summary>
    /// Set of patterns to run. 
    /// </summary>
    public List<Pattern> patterns;

    //TODO move to duration wave class. 
    public float Duration = 5;

    //for safty  
    private void Start()
    {
        gameObject.SetActive(false); 
    }

    private void Update()
    {
        Duration -= Time.deltaTime; 

        if(Duration < 0)
        {
            Stop(); 
        }
    }

    /// <summary>
    /// Runs itself then all nested patterns. 
    /// </summary>
    public override void Run()
    {
        base.Run(); 
        foreach (Pattern p in patterns)
        {
            p.Run();
        }
    }

    /// <summary>
    /// Stops all nested pattern then itself. 
    /// </summary>
    public override void Stop()
    {
        foreach (Pattern p in patterns)
        {
            p.Stop();
        }
        base.Stop();
    }
}
