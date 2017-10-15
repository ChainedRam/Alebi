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
    public sealed override bool ShouldGenerate()
    { 
        return !HasGenerated;
    }

    protected override void SetupGenerator()
    {
        base.SetupGenerator();
        HasGenerated = false;
        OnGenerate += () => HasGenerated = true;
    }
    #endregion
}
