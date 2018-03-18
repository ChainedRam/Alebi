using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Projection
{
    public class LinearMotion : AbsoluteMotion
    {
        public float Speed;

        public override Vector2 GetRelativeOffset(Vector2 defaultVector)
        {
            return new Vector2(0, Speed) + defaultVector;
        }
    }
}
