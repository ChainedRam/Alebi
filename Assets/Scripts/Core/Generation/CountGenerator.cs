using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountGenerator : GeneratorWrap
{
    [Header("CountGenerator")]
    public int MaxGeneration;

    private int CurrentGeneration;

    private void Awake()
    {
        OnStartGenerating += () => CurrentGeneration = 0; 

        OnGenerate += () => CurrentGeneration++;
    }


    public override bool ShouldGenerate()
    {
        return CurrentGeneration < MaxGeneration;
    }
}
