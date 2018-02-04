using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RelativeMotion : Motion
{
    public override Vector2 Default()
    {
        return Vector2.up; 
    }
}
