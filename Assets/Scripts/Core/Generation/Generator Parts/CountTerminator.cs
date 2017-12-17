﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A terminatorgenerates based on a given number before stopping. 
/// </summary>
public class CountTerminator : GeneratorTerminator
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
    #region Override GeneratorTerminator
    public override void Setup(Generator gen)
    {
        CurrentGeneration = 0;

        gen.OnGenerate -= IncrementCounter;
        gen.OnGenerate += IncrementCounter; 

    }

    public override void SetApart(Generator gen)
    {
        gen.OnGenerate -= IncrementCounter;
    }

    public override bool ShouldTerminate(Generator gen)
    {
        return CurrentGeneration >= MaxGeneration;
    }
    #endregion

    private void IncrementCounter()
    {
        CurrentGeneration++;
    }

}