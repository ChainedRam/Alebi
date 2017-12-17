using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownCondition : GeneratorCondition
{
    [Tooltip("Time to wait in seconds.")]
    [Range(0, 1000)]
    public float WaitTime;

    private float CurrentTimer;

    public override void OnGeneratorGenerated()
    {
        CurrentTimer = WaitTime; 
    }

    public override bool ShouldGenerate(Generator gen)
    {
        return (CurrentTimer -= Time.deltaTime) <= 0;
    }
 
}
