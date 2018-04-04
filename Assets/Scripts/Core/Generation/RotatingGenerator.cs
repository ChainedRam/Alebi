using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingGenerator : TimedGenerator
{
    public GameObject Target;
    public int Loops; 
    
    public float TargetAngle;
    private float AngularSpeed;
    private float CurrentAngle; 

    protected override void OnBegin()
    {
        base.OnBegin();
        CurrentAngle = Target.transform.localEulerAngles.z;
        TargetAngle = 360 * Loops + CurrentAngle;  
        AngularSpeed = Time.fixedDeltaTime * TargetAngle / (WaitTime);
    }

    protected override void OnGenerate(GenerateEventArgs e)
    {
        if (Mathf.Abs(TargetAngle) - Mathf.Abs(CurrentAngle) <= Mathf.Abs(AngularSpeed))
        {
            CurrentAngle = TargetAngle;
        }
        else
        {
            CurrentAngle += AngularSpeed; 
        }
        
        Target.transform.localEulerAngles = Vector3.forward * CurrentAngle;
    }

    protected override bool ShouldGenerate()
    {
        return true;  
    }
}
