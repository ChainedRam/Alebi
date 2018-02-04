using ChainedRam.Core.Extentions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMotion : RelativeMotion
{
    public float AngleOffset;
    private float CurrentAngleOffset;

    public override void Initialize(float delta)
    {
        CurrentAngleOffset = 0; 
    }

    public override Vector2 GetRelativeOffset(Vector2 defaultVector)
    {
        CurrentAngleOffset += AngleOffset;
        return defaultVector.Rotate(CurrentAngleOffset);
    }
}
