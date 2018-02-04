using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratedMotion : AbsoluteMotion
{
    public float Acceleration;
    private float CurrentAcceleration;

    public override Vector2 GetRelativeOffset(Vector2 defaultVector)
    {
        CurrentAcceleration += Acceleration;
        return new Vector2(0, CurrentAcceleration) + defaultVector;
    }

    public override void Initialize(float delta)
    {
        CurrentAcceleration = 0;
    }

}
