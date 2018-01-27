using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratedMotion : Motion
{
    public float Acceleration;
    private float CurrentAcceleration;

    public override Vector2 GetOffset()
    {
        CurrentAcceleration += this.Acceleration;
        Vector2 acceleration = new Vector2(0, CurrentAcceleration);
        return acceleration;
    }

    public override void Initialize()
    {
        CurrentAcceleration = 0;
    }
}
