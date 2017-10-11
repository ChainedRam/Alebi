using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorWrap : Generator
{
    [Header("GeneratorWrap")]
    public Generator Wrapped;

    private void Awake()
    {
        Attach(Wrapped); 
    }

    public override void Generate()
    {
        Wrapped.Generate();
    }

    public override void SkippedGeneration()
    {
        Wrapped.SkippedGeneration();
    }

    public override bool ShouldGenerate()
    {
        return Wrapped.ShouldGenerate();
    }
}
