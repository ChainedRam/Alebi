using ChainedRam.Alebi.Interface.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ChainedRam.Core.Projection
{
    /// <summary>
    /// Core implementation of the <see cref="IRunnable"/> interface.   
    /// </summary>
    public abstract class Runnable : MonoBehaviour, IRunnable
    {
        #region IRunnable Events 
        public event Action OnRun;
        public event Action OnStop;
        #endregion

        #region IRunnable Methods 
        public virtual void Run()
        {
            OnRun?.Invoke();
        }
        public virtual void Stop()
        {
            OnStop?.Invoke();
        }
        #endregion
    }
}
