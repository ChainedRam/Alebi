using ChainedRam.Core.Extentions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Projection
{
    /// <summary>
    /// Represent an object that gives an offset value as a direction to create motional movment. 
    /// </summary>
    public abstract class Motion : MonoBehaviour
    {
        #region Public Abstract
        public abstract Vector2 GetRelativeOffset(Vector2 defaultVector);
        public abstract Vector2 Default();
        #endregion
        #region Public Virtual 
        public virtual Vector2 GetOffset()
        {
            return GetRelativeOffset(Default());
        }

        public virtual void Initialize(float delta = 1) { }
        #endregion
    }
}