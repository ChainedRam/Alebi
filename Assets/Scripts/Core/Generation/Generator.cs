using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// A Compnent that genrates. 
/// </summary>
public abstract class Generator : MonoBehaviour
{
    #region Inspector Attributes
    [Header("Generator")]
    public bool IsGenerating;
    #endregion
    #region Public Events 
    /// <summary>
    /// Invoked when generating. <see cref="Generate"/>
    /// </summary>
    public event Action OnGenerate;

    /// <summary>
    /// Invoked when Starting generation. <see cref="StartGenerating"/>
    /// </summary>
    public event Action OnStartGenerating;

    /// <summary>
    /// Invoked when stoping generation. <see cref="StopGenerating"/>
    /// </summary>
    public event Action OnStopGenerating;
    #endregion
    #region Public Methods 
    /// <summary>
    /// Generates something. 
    /// </summary>
    public void Generate()
    {
        OnGenerate?.Invoke(); 
    }

    /// <summary>
    /// Starts Generation and invokes OnStart events.
    /// </summary>
    public void StartGenerating()
    {
        IsGenerating = true;
        OnStartGenerating?.Invoke();
    }

    /// <summary>
    /// Stops Generation and trigger OnStop events.
    /// </summary>
    public void StopGenerating()
    {
        IsGenerating = false;
        OnStopGenerating?.Invoke();
    }
    #region Attach Detach Methods
    //TODO check recursion for all add  

    /// <summary>
    /// Adds follower's as a listener when Start. 
    /// </summary>
    /// <param name="follower"></param>
    /// <remarks>To avoid recursion, followe <see cref="DetachOnStart(Generator)"/> the calling generator. </remarks>
    public void AttachOnStart(Generator follower)
    {
        //avoid recursion 
        follower.DetachOnStart(this); 

        OnStartGenerating += follower.StartGenerating; 
    }

    /// <summary>
    /// Removes following's StartGenerating from Start event list. 
    /// </summary>
    /// <param name="follower"></param>
    public void DetachOnStart(Generator follower)
    {
        OnStartGenerating -= follower.StartGenerating;
    }

    /// <summary>
    /// Adds follower as listern when stop. 
    /// </summary>
    /// <param name="follower"></param>
    public void AttachOnStop(Generator follower)
    {       
        //avoid recursion 
        follower.DetachOnStop(this); 
        
        OnStopGenerating += follower.StopGenerating;
    }

    /// <summary>
    /// Removes follower from Stop event list. 
    /// </summary>
    /// <param name="follower"></param>
    public void DetachOnStop(Generator follower)
    {
        OnStopGenerating -= follower.StopGenerating;
    }

    /// <summary>
    /// Attaches follower to genrate event. 
    /// </summary>
    /// <param name="follower"></param>    
    public void AttachOnGenerate(Generator follower)
    {
        //avoid recursion 
        follower.DetachOnGenerate(this);

        OnGenerate += follower.OnGenerate;
    }

    /// <summary>
    /// Removes follower from on genrate event list. 
    /// </summary>
    /// <param name="follower"></param>
    public void DetachOnGenerate(Generator follower)
    {
        OnGenerate -= follower.OnGenerate;
    }

    /// <summary>
    /// Attachs follower's OnStart, OnStop, & OnGenerate events to own events, 
    /// </summary>
    /// <param name="follower"></param>
    public void Attach(Generator follower)
    {
        AttachOnStart(follower);
        AttachOnStop(follower);
        AttachOnGenerate(follower); 
    }

    /// <summary>
    /// Detaches follower's OnStart, OnStop, & OnGenerate events from own events, 
    /// </summary>
    /// <param name="follower"></param>
    public void Detach(Generator follower)
    {
        DetachOnStart(follower);
        DetachOnStop(follower);
        DetachOnGenerate(follower);
    }
    #endregion
    #endregion
    #region Unity Methods 
    /// <summary>
    /// Runs the generation cycle. 
    /// </summary>
    private void Update()
    {
        if (!IsGenerating)
        {
            return;
        }

        if (ShouldGenerate())
        {
            Generate();
            OnGenerate?.Invoke();
        }
        else
        {
            SkippedGeneration(); 
        }
    }
    #endregion
    #region Abstract Methods
    /// <summary>
    /// Called with in <see cref="Update"/> to check if generation is allowed.
    /// </summary>
    /// <returns></returns>
    public abstract bool ShouldGenerate();

    /// <summary>
    /// Called when generate call skips a cycle because <see cref="ShouldGenerate"/> returns false. 
    /// </summary>
    public virtual void SkippedGeneration()
    {
        StopGenerating(); 
    }

    #endregion
}
