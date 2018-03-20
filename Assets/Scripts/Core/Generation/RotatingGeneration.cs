using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingGeneration : TimedGenerator
{
    public GameObject Target; 
    public float TargetAngle; 

    public float Speed;

    protected override void OnBegin()
    {
        base.OnBegin();
        Speed = Time.fixedDeltaTime * (TargetAngle - Target.transform.localEulerAngles.z) / (WaitTime);
    }

    protected override void OnGenerate(GenerateEventArgs e)
    {
        if (Mathf.Abs(TargetAngle - Target.transform.localEulerAngles.z) <= Mathf.Abs(Speed))
        {
            Target.transform.localEulerAngles = Vector3.forward * TargetAngle;
        }
        else
        {
            Target.transform.localEulerAngles += Vector3.forward * Speed;
        }
        
    }

    protected override bool ShouldGenerate()
    {
        return true; 
    }
}
