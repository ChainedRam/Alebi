using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wraps a generator around it. Can be used to refrence other generator. 
/// </summary>
public class GeneratorWrap : Generator
{
    #region Inspector Attribute 
    [Header("GeneratorWrap")]
    public Generator Wrapped;
    #endregion
    #region Unity Methods 
    protected virtual void Awake()
    {
        Attach(Wrapped); 
    }
    #endregion
    #region Generator Overrides 

    public override bool ShouldGenerate()
    {
        return Wrapped.ShouldGenerate();
    }
    #endregion
}
