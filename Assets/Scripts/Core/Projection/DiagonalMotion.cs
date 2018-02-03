using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainedRam.Core.Extentions;

public class DiagonalMotion : Motion
{
    [Range(-90, 90)]
    public float InitAngle;

    public override Vector2 GetOffset()
    {
        Vector2 dir = new Vector2(0,1);
        Vector2 offset = dir.Rotate(-InitAngle);
        return offset;
    }

    public override Vector2 Apply(Vector2 vector)
    {
        return vector.Rotate(-InitAngle);
    }
}
