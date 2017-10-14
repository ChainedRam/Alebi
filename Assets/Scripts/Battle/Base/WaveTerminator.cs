using ChainedRam.Alebi.Battle;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Battle
{
    /// <summary>
    /// Terminates a wave under centain consition 
    /// </summary>
    public abstract class WaveTerminator : MonoBehaviour
    {
        /// <summary>
        /// Wave to be terminated //TODO attemp to grab from compnent. Use that thing. 
        /// </summary>
        public Wave Wave;

        /// <summary>
        /// [TODO private] 
        /// </summary>
        public bool IsTerminating;

        private void Start()
        {
            IsTerminating = false;
        }

        /// <summary>
        /// Treminates wave 
        /// </summary>
        public void Terminate()
        {
            IsTerminating = true;
            Wave.StopGenerating();

            OnTerminate();
        }

        /// <summary>
        /// Tests terminating consition. 
        /// </summary>
        void Update()
        {
            if (!IsTerminating && ShouldTerminate())
            {
                Terminate();
            }
        }

        /// <summary>
        /// Sets up terminator 
        /// </summary>
        /// <param name="Patterns"></param>
        public virtual void Setup(Wave wave,Pattern[] Patterns)
        {
            Wave = wave; 
            IsTerminating = false;
        }

        /// <summary>
        /// True if Wave should terminate 
        /// </summary>
        /// <returns></returns>
        public abstract bool ShouldTerminate();

        /// <summary>
        /// Helpers function that allows to listen to ever projected Projectile in wave 
        /// </summary>
        /// <param name="launcher"></param>
        /// <param name="proj"></param>
        public virtual void OnProjectileLaunched(Pattern launcher, Projectile proj)
        {
            //do nothing 
        }

        /// <summary>
        /// Gets called after terminating. 
        /// </summary>
        public virtual void OnTerminate()
        {
            //do nothing 
        }

    }
}
