using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates wrapped generator once. ONCE! 
/// </summary>
public abstract class OnceGenerator : GeneratorWrap
{
    #region Private Atributes 
    private bool HasGenerated;
    #endregion
    #region Unity Methods 
    #endregion
    #region GeneratorWrap Override 
    protected sealed override bool ShouldGenerate()
    { 
        return !HasGenerated;
    }

    protected override void WhenAwake()
    {
        base.WhenAwake();
        HasGenerated = false;
        OnGenerate += () => HasGenerated = true; //REEEEEEE. move to WhenGenerate
    }
    #endregion
}
