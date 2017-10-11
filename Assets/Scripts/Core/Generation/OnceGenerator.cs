using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnceGenerator : GeneratorWrap
{
    private bool HasGenerated;

    private void Awake()
    {
        HasGenerated = false;
    }

    public sealed override void Generate()
    {
        base.Generate();
        HasGenerated = true;
    }

    public sealed override bool ShouldGenerate()
    {
        return !HasGenerated; 
    }

}
