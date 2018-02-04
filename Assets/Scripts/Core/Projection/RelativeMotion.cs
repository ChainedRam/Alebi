using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Projection
{
    public abstract class RelativeMotion : Motion
    {
        public override Vector2 Default()
        {
            return Vector2.up; 
        }
    }
}
