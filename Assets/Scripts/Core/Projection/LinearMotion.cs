using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMotion : Motion
{
    public float Speed;

    public override Vector2 GetOffset()
    {
        Vector2 speed = new Vector2(0, Speed * Time.fixedDeltaTime);
        return speed;
    }
}
