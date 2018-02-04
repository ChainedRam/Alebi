using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainedRam.Core.Extentions;

public class DiagonalMotion : RelativeMotion
{
    [Range(-90, 90)]
    public float InitAngle;

    public override Vector2 GetRelativeOffset(Vector2 vector)
    {
        return vector.Rotate(-InitAngle);
    }

}
