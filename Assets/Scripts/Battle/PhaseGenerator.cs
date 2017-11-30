using ChainedRam.Alebi.Battle;
using ChainedRam.Alebi.Core;
using ChainedRam.Alebi.Interface;
using ChainedRam.Core.Generation;
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
    public abstract class PhaseGenerator : NestedGenerator<Phase>
    {
        /// <summary>
        /// Holds corrent running phase (for debugging)
        /// </summary>
        public Phase CurrentPhase;

        /// <summary>
        /// Holds current phase index 
        /// </summary>
        protected int CurrentPhaseIndex;

        protected override void WhenAwake()
        {
            base.WhenAwake();
            CurrentPhaseIndex = -1;
        }

        /// <summary>
        /// Starts next phase and stops previous one. 
        /// </summary>
        public void NextPhase()
        {
           /* if (CurrentPhase != null)
            {
                CurrentPhase.StopGenerating();
            }

            if (CurrentPhaseIndex < Phases.Length)
            {
                CurrentPhase = Phases[++CurrentPhaseIndex];
            }
            else
            {
                Defeated();
            }*/
        }

        /*
        /// <summary>
        /// Recieve damage from projectile  
        /// </summary>
        /// <param name="holder"></param>
        public sealed override void Recieve(IHolder<T> holder)
        {
            OnRecieve(holder);

            if (ShouldSwitchPhase())
            {
                PhaseGenerator.Generate(); 
                return;
            }
        }

        protected abstract void OnRecieve(IHolder<T> holder);
        */
        public bool ShouldSwitchPhase()
        {
            return true; 
        }

    }
}
