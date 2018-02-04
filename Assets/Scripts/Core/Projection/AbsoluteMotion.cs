using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Projection
{
    public abstract class AbsoluteMotion : Motion
    {
        public override Vector2 Default()
        {
            return Vector2.zero;
        }
    } 
}
