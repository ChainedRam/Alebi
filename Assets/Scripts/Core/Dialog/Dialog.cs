using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ChainedRam.Core.Dialog
{
    /// <summary>
    /// Represent a dialog object that contains dialog life cycle defined in <see cref="DialogBox"/>
    /// </summary>
    public abstract class Dialog : MonoBehaviour
    {
        /// <summary>
        /// Holds flags for pausing dialog 
        /// </summary>
        public abstract DialogPauseType Property { get; }

        /// <summary>
        /// Resets dialog pointer
        /// </summary>
        public abstract void ResetDialog();

        /// <summary>
        /// Returns next charachter tobe displayed 
        /// </summary>
        /// <returns></returns>
        public abstract char NextCharachter();

        /// <summary>
        /// False when dialog runs out of charachters. Otherwise, true. 
        /// </summary>
        /// <returns></returns>
        public abstract bool HasNext();

        public event Action OnStart;
        public event Action OnEnd;
        public event Action OnPause;
        public event Action OnResume;

        /// <summary>
        /// Invoked when dialog starts
        /// </summary>
        public virtual void WhenDialogStart()
        {
            OnStart?.Invoke(); 
        }

        /// <summary>
        /// Invoked when dialog ends 
        /// </summary>
        public virtual void WhenDialogEnd()
        {
            OnEnd?.Invoke();
        }

        /// <summary>
        /// Invoked when dialog is paused. <see cref="DialogPauseType"/>
        /// </summary>
        public virtual void WhenDialogPause()
        {
            OnPause?.Invoke();
        }

        /// <summary>
        /// Invoked when dialog resumes after pausing. 
        /// </summary>
        public virtual void WhenDialogResume()
        {
            OnResume?.Invoke();
        }

        /// <summary>
        /// Gets time delay in seconds between displaying each charachter. 
        /// </summary>
        /// <returns>In seconds</returns>
        public abstract float GetDisplayDelay();
    }
}
