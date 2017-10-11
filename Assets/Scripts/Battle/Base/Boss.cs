using ChainedRam.Alebi.Battle;
using ChainedRam.Alebi.Core;
using ChainedRam.Alebi.Interface;
using ChainedRam.Core.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Battle
{
    /// <summary>
    /// Basic boss definition 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Boss<T> : Collider2DReciever<T>
    {
        /// <summary>
        /// Set of phases 
        /// </summary>
        public Phase[] Phases;

        /// <summary>
        /// Holds corrent running phase (for debugging)
        /// </summary>
        public Phase CurrentPhase;

        /// <summary>
        /// Holds current phase index 
        /// </summary>
        protected int CurrentPhaseIndex;

        private void Awake()
        {
            CurrentPhaseIndex = -1;
        }

        /// <summary>
        /// Starts next phase and stops previous one. 
        /// </summary>
        public void NextPhase()
        {
            if (CurrentPhase != null)
            {
                CurrentPhase.Stop();
            }

            if (CurrentPhaseIndex < Phases.Length)
            {
                CurrentPhase = Phases[++CurrentPhaseIndex];
            }
            else
            {
                Defeated(this);
            }
        }

        /// <summary>
        /// Recieve damage from projectile  
        /// </summary>
        /// <param name="holder"></param>
        public sealed override void Recieve(IHolder<T> holder)
        {
            OnRecieve(holder);

            if (ShouldSwitchPhase(this))
            {
                NextPhase();
                return;
            }
        }

        protected abstract void OnRecieve(IHolder<T> holder);

        public abstract bool ShouldSwitchPhase(Boss<T> boss);

        public abstract void Defeated(Boss<T> boss); 
    }
}
