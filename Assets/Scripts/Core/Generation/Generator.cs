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
    public bool PrintDebug; 
    #endregion
    #region Public Events 
    /// <summary>
    /// Invoked when generating. <see cref="Generate"/>
    /// </summary>
    public event Action OnGenerate;

    /// <summary>
    /// Invoked when stoping generation. <see cref="SkippedGenerate"/>
    /// </summary>
    public event Action OnSkippedGenerate;

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
        Log("Generate"); 
    }

    /// <summary>
    /// Called when generate call skips a cycle because <see cref="ShouldGenerate"/> returns false. 
    /// </summary>
    public void SkippedGenerate()
    {
        OnSkippedGenerate?.Invoke();
        SkippedGeneration();
        //Log("Skipped");
    }

    /// <summary>
    /// Starts Generation and invokes OnStart events.
    /// </summary>
    public void StartGenerating()
    {
        IsGenerating = true;
        OnStartGenerating?.Invoke();
        Log("Started");
    }

    /// <summary>
    /// Stops Generation and trigger OnStop events.
    /// </summary>
    public void StopGenerating()
    {
        IsGenerating = false;
        OnStopGenerating?.Invoke();
        Log("Stopped ");
    }
    #region Attach Detach Methods
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

        //avoid duplicate
        DetachOnGenerate(follower); 

        OnGenerate += follower.Generate;
    }

    /// <summary>
    /// Removes follower from on genrate event list. 
    /// </summary>
    /// <param name="follower"></param>
    public void DetachOnGenerate(Generator follower)
    {
        OnGenerate -= follower.Generate;
    }

    /// <summary>
    /// Attaches follower to genrate event. 
    /// </summary>
    /// <param name="follower"></param>    
    public void AttachOnSkip(Generator follower)
    {
        //avoid recursion 
        follower.DetachOnSkip(this);

        OnSkippedGenerate += follower.SkippedGenerate;
    }

    /// <summary>
    /// Removes follower from on genrate event list. 
    /// </summary>
    /// <param name="follower"></param>
    public void DetachOnSkip(Generator follower)
    {
        OnSkippedGenerate -= follower.SkippedGenerate;
    }

    /// <summary>
    /// Attachs follower's OnStart, OnStop,OnGenerate, & OnSkip events to own events, 
    /// </summary>
    /// <param name="follower"></param>
    public void Attach(Generator follower)
    {
        AttachOnGenerate(follower);
        AttachOnSkip(follower); 
        AttachOnStart(follower);
        AttachOnStop(follower);
    }

    /// <summary>
    /// Detaches follower's OnStart, OnStop,OnGenerate, & OnSkip events from own events, 
    /// </summary>
    /// <param name="follower"></param>
    public void Detach(Generator follower)
    {
        DetachOnGenerate(follower);
        DetachOnSkip(follower);
        DetachOnStart(follower);
        DetachOnStop(follower);
    }
    #endregion
    #endregion
    #region Unity Methods 
    protected void Awake()
    {
        SetupGenerator(); 
    }

    /// <summary>
    /// Runs the generation cycle. 
    /// </summary>
    protected void Update()
    {
        if (!IsGenerating)
        {
            return;
        }

        if (ShouldGenerate())
        {
            Generate();
        }
        else
        {
            SkippedGenerate(); 
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
    /// Called last in <see cref="SkippedGenerate"/>. By default, stops generation. 
    /// </summary>
    protected virtual void SkippedGeneration()
    {
        StopGenerating(); //TODO think about making it a bool 
    }

    protected virtual void SetupGenerator()
    {

    }

    protected void Log(string s)
    {
        if (PrintDebug)
        {
            Debug.Log(s+ " " + name);
        }
    }
    #endregion
}
