using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Generation;
using UnityEngine;

public class MultiProjectileGenerator : ProjectileGenerator
{
    [Header("Multi Projectile Generator")]
    public int Count;
    
    public float FireRate; 

    private Coroutine FiringCoroutine; 

    protected override void OnGenerate(GenerateEventArgs e)
    {
        FiringCoroutine = StartCoroutine(Firing(e));
    }

    private IEnumerator Firing(GenerateEventArgs e)
    {
        for (int i = 0; i < Count; i++)
        {
            base.OnGenerate(e);

            yield return new WaitForSeconds(FireRate);
        }
    }

    protected override float GetSyncedWaitTime()
    {
        return FireRate * Count;
    }

    protected override void OnEnd()
    {
        base.OnEnd();
        
        if(FiringCoroutine != null)
        {
            StopCoroutine(FiringCoroutine); 
        }

        
    }
}
