using ChainedRam.Core.Extentions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMotion : Motion
{
    public float linearSpeed;
    public bool IsCurve = true;
    public bool IsAcceleration = true;
    public float angleOffset;
    private float currentAngleOffset;

    [Range(-90, 90)]
    public float initAngle;

    public override Vector2 GetOffset()
    {
        angleControl();
        Vector2 offset = new Vector2(0, linearSpeed * Time.fixedDeltaTime);
        offset = offset.Rotate(-initAngle + currentAngleOffset);
        return offset;
    }

    public override void Initialize()
    {
        currentAngleOffset = 0; 
    }

    void angleControl()
    {
        if (IsCurve)
        {

            currentAngleOffset += angleOffset;
        }
    }

}
