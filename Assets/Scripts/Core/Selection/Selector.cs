using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Selection
{
    /// <summary>
    /// Compnenet that implements a select method to select an element from a list. 
    /// </summary>
    public abstract class Selector : MonoBehaviour
    {
        protected void Start()
        {
            ResetSelector();
        }

        public virtual void ResetSelector() {} 
        public abstract T Select<T>(T[] list, T prev = null) where T : class;
    }
}
