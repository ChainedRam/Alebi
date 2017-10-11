using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Generator : MonoBehaviour
{
    [Header("Generator")]
    public bool IsGenerating;

    public event Action OnGenerate;

    public event Action OnStartGenerating;
    public event Action OnStopGenerating;

    public void StartGenerating()
    {
        IsGenerating = true;
        OnStartGenerating?.Invoke();
    }

    public void StopGenerating()
    {
        IsGenerating = false;
        OnStopGenerating?.Invoke();
    }

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

    public abstract void Generate();
    public abstract void SkippedGeneration(); 
    public abstract bool ShouldGenerate();


    public void AttachOnStart(Generator follower)
    {
        OnStartGenerating += follower.StartGenerating; 
    }

    public void DetachOnStart(Generator follower)
    {
        OnStartGenerating -= follower.StartGenerating;
    }

    public void AttachOnStop(Generator follower)
    {
        OnStopGenerating += follower.StopGenerating;
    }

    public void DetachOnStop(Generator follower)
    {
        OnStopGenerating -= follower.StopGenerating;
    }

    public void Attach(Generator follower)
    {
        AttachOnStart(follower);
        AttachOnStop(follower);
    }

    public void Detach(Generator follower)
    {
        DetachOnStart(follower);
        DetachOnStop(follower);
    }

}
