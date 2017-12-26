using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represent a dialog object that contains dialog life cycle defined in <see cref="DialogBox"/>
/// </summary>
public abstract class Dialog : MonoBehaviour
{
    /// <summary>
    /// Holds flags for pausing dialog 
    /// </summary>
    public abstract DialogPauseProperty Property { get; }

    /// <summary>
    /// Resets dialog pointer
    /// </summary>
    public abstract void ResetDialog();

    /// <summary>
    /// Returns next charachter tobe displayed 
    /// </summary>
    /// <returns></returns>
    public abstract char NextCharachter();

    /// <summary>
    /// False when dialog runs out of charachters. Otherwise, true. 
    /// </summary>
    /// <returns></returns>
    public abstract bool HasNext();

    /// <summary>
    /// Invoked when dialog starts
    /// </summary>
    public virtual void WhenDialogStart() { }

    /// <summary>
    /// Invoked when dialog ends 
    /// </summary>
    public virtual void WhenDialogEnd() { }

    /// <summary>
    /// Invoked when dialog is paused. <see cref="DialogPauseProperty"/>
    /// </summary>
    public virtual void WhenDialogPause() { }

    /// <summary>
    /// Invoked when dialog resumes after pausing. 
    /// </summary>
    public virtual void WhenDialogResume() { }

    /// <summary>
    /// Gets time delay in seconds between displaying each charachter. 
    /// </summary>
    /// <returns>In seconds</returns>
    public abstract float GetDisplayDelay(); 
}
