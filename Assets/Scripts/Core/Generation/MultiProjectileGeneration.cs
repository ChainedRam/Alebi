using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Generation;
using UnityEngine;

public class MultiProjectileGeneration : ProjectileGeneration
{ 
    public int Count;
    
    public float FireRate; 

    public override void Trigger(Generator sender)
    {
        StartCoroutine(Firing(sender));
    }

    private IEnumerator Firing(Generator sender)
    {
        for (int i = 0; i < Count; i++)
        {
            base.Trigger(sender);

            yield return new WaitForSeconds(FireRate);
        }
    }

    protected override void SyncWaitTime()
    {
        WaitTime = FireRate * Count; 
    }
}
