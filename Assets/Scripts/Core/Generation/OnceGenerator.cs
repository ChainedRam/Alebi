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
    private void Awake()
    {
        HasGenerated = false;
        OnGenerate += () => HasGenerated = true;
    }
    #endregion
    #region GeneratorWrap Override 
    public sealed override bool ShouldGenerate()
    { 
        return !HasGenerated;
    }
    #endregion
}
