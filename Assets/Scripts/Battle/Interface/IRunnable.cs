using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Interface.Battle
{
    /// <summary>
    /// An object that can run and stop with an event accosiated with each. 
    /// </summary>
    public interface IRunnable
    {
        //TODO please delete me 
        #region Methods
        /// <summary>
        /// Starts running. 
        /// </summary>
        void Run();

        /// <summary>
        /// Stops what was ran. 
        /// </summary>
        void Stop();
        #endregion 
        #region Events
        /// <summary>
        /// Invoked when <see cref="Run"/> is called 
        /// </summary>
        event Action OnRun;

        /// <summary>
        /// Invoked when <see cref="Stop"/> is called 
        /// </summary>
        event Action OnStop;
        #endregion
    }
}
