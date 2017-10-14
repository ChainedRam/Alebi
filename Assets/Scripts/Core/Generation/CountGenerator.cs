using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wraps a generator and generates based on a given number before stopping. 
/// </summary>
public class CountGenerator : GeneratorWrap
{
    #region Inspector Attributes 
    [Header("CountGenerator")]
    [Tooltip("Maximum number to generate")]
    public int MaxGeneration;
    #endregion
    #region Private Attributes 
    /// <summary>
    /// Holds counter 
    /// </summary>
    private int CurrentGeneration;
    #endregion
    #region Unity Methods
    private void Awake()
    {
        OnStartGenerating += () => CurrentGeneration = 0; 

        OnGenerate += () => CurrentGeneration++;
    }
    #endregion
    #region GeneratorWrap Override 
    public override bool ShouldGenerate()
    {
        return CurrentGeneration < MaxGeneration;
    }
    #endregion
}
