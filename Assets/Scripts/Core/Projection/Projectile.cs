using ChainedRam.Alebi.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Projection
{
    /// <summary>
    /// A moving unit that contained a rigide body. 
    /// </summary>
    public abstract class Projectile : MonoBehaviour
    {
        public abstract void Setup(float speed, float direction);
    }
}
