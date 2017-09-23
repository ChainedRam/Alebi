using ChainedRam.Alebi.Interface.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ChainedRam.Alebi.Core
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
            gameObject.SetActive(true); 
            if(OnRun != null)
            {
                OnRun(); 
            }
        }
        public virtual void Stop()
        {
            gameObject.SetActive(false);
            if (OnStop != null)
            {
                OnStop();
            }
        }
        #endregion
    }
}
