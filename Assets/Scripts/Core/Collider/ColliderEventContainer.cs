using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ChainedRam.Core.Collider
{
    /// <summary>
    /// Holds Enter and Exit event as unity events. 
    /// </summary>
    public class ColliderEventContainer : MonoBehaviour
    {
        #region Inspecter Variables 
        public UnityEvent OnEnter;
        public UnityEvent OnExit;
        #endregion
    }
}
