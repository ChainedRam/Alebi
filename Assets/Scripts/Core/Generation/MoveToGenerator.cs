using System;
using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Generation;
using UnityEngine;

[Obsolete("Use: ShowupOnScreenGenerator")]
public class MoveToGenerator : TimedGenerator
{
    public GameObject Movable;

    public Transform Destenation;

    [ContextMenuItem("Sync", "SyncSpeed")]
    public float Speed;

    protected bool IsMoving;
    private const float DistanceDalta = 0.01f; 

    protected override void OnGenerate(GenerateEventArgs e)
    {
        IsMoving = true;   
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate(); 

        if (!IsMoving)
        {
            return;
        }

        Movable.transform.position = Vector3.MoveTowards(Movable.transform.position, Destenation.position, Speed * Time.fixedDeltaTime);

        IsMoving = Vector3.Distance(Movable.transform.position, Destenation.position) > DistanceDalta; 
      
    }

    protected override float GetSyncedWaitTime()
    {
       return Vector3.Distance(Movable.transform.position, Destenation.position) / Speed; 
    }

    private void SyncSpeed()
    {
        Speed = Vector3.Distance(Movable.transform.position, Destenation.position) / WaitTime;
    }

}
