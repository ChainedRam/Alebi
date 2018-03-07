using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedGenerator : Generator
{
    [Header("Timed Generator")]

    [ContextMenuItem("Sync", "SyncWaitTime")]
    public float WaitTime;

    private float CurrentTime;
    private bool Once; 

    protected override bool ShouldGenerate()
    {
        if (Once)
        {
            Once = false;
            return true;
        }
        else
        {
            return false; 
        }
    }

    protected override void Update()
    {
        base.Update(); 
        if(CurrentTime > 0)
            CurrentTime -= Time.fixedDeltaTime; 
    }

    protected override bool ShouldTerminate()
    {
       return CurrentTime <= 0;
    }
    
    protected override void OnBegin()
    {
        base.OnBegin();
        CurrentTime = WaitTime;
        Once = true; 
    }

    protected override void Start()
    {
        base.Start();
        CurrentTime = WaitTime;
        Once = true; 
    }

    private void SyncWaitTime()
    {
        WaitTime = GetSyncedWaitTime(); 
    }

    protected virtual float GetSyncedWaitTime()
    {
        return 1f; 
    }
}
