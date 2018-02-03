using ChainedRam.Core.Extentions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMotion : Motion
{
    public float AngleOffset;
    private float CurrentAngleOffset;
    public override Vector2 GetOffset()
    {
        CurrentAngleOffset += AngleOffset;
        Vector2 offset = new Vector2(0, 1);
        offset = offset.Rotate(CurrentAngleOffset);
        return offset;
    }

    public override void Initialize()
    {
        CurrentAngleOffset = 0; 
    }

    public override Vector2 Apply(Vector2 vector)
    {
        CurrentAngleOffset += AngleOffset;
        return vector.Rotate(CurrentAngleOffset);
    }
}
