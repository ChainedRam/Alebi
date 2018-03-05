using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Generation;
using UnityEngine;

public class MoveToGeneration : WaitGeneration
{
    public GameObject Movable;

    public Transform Destenation;

    [ContextMenuItem("Sync", "SyncSpeed")]
    public float Speed;

    protected bool IsMoving;
    private const float DistanceDalta = 0.01f; 

    public override void Trigger(Generator sender)
    {
        IsMoving = true;   
    }

    private void FixedUpdate()
    {
        if (!IsMoving)
        {
            return;
        }

        Movable.transform.position = Vector3.MoveTowards(Movable.transform.position, Destenation.position, Speed * Time.fixedDeltaTime);

        IsMoving = Vector3.Distance(Movable.transform.position, Destenation.position) > DistanceDalta; 
      
    }

    protected override void SyncWaitTime()
    {
       WaitTime = Vector3.Distance(Movable.transform.position, Destenation.position) / Speed; 
    }
    private void SyncSpeed()
    {
        Speed = Vector3.Distance(Movable.transform.position, Destenation.position) / WaitTime;
    }

}
