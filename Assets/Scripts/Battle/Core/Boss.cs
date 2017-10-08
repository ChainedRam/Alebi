using ChainedRam.Alebi.Battle;
using ChainedRam.Alebi.Core;
using ChainedRam.Alebi.Interface;
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
    public abstract class Boss<T> : DamageReciever<T>
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

            CurrentPhase = Phases[++CurrentPhaseIndex];
        }

        /// <summary>
        /// Recieve damage from projectile  
        /// </summary>
        /// <param name="dmg"></param>
        public override void RecieveDamage(IDamage<T> dmg)
        {
            if (ShouldSwitchPhase(this))
            {
                NextPhase();
                return;
            }
        }

        public abstract bool ShouldSwitchPhase(Boss<T> boss);
    }
}
