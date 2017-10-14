using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wrap a generater and stops after a given time. 
/// </summary>
public class TimedGeneratorContainer : GeneratorWrap
{
    #region Inspector Attributes
    [Header("TimedGeneratorContainer")]

    [Tooltip("Seconds")]
    public float Delay;
    #endregion
    #region Private Attributes
    private float CurrentTime;
    #endregion
    #region Unity Methods
    private void Awake()
    {
        OnStartGenerating += () => CurrentTime = 0;
        OnGenerate += () => CurrentTime = 0; 
    }
    #endregion
    #region Override GeneratorWrap
    public override bool ShouldGenerate()
    {
        return ((CurrentTime += Time.deltaTime) >= Delay && Wrapped.ShouldGenerate()); 
    }
    #endregion
}
