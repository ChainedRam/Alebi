using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedGeneratorContainer : GeneratorWrap
{
    [Header("TimedGeneratorContainer")]
    public float Delay;

    private float CurrentTime;

    private void Awake()
    {
        OnStartGenerating += () => CurrentTime = 0;
        OnGenerate += () => CurrentTime = 0; 
    }

    public override bool ShouldGenerate()
    {
        return ((CurrentTime += Time.deltaTime) <= Delay && base.ShouldGenerate()); 
    }
}
