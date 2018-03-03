using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitGeneration : TimedGeneration
{
    [ContextMenuItem("Sync", "SyncWaitTime")]
    public float WaitTime; 

    public override float Seconds
    {
        get
        {
            return WaitTime; 
        }
    }

    public override void Trigger(Generator sender)
    {
        
    }

    protected virtual void SyncWaitTime()
    {
        WaitTime = 1; 
    }
}
