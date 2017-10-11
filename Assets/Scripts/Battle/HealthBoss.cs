using ChainedRam.Alebi.Interface;
using ChainedRam.Core.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Battle
{
    /// <summary>
    /// A boss with integre as it's health. 
    /// </summary>
    public class HealthBoss : Boss<int>
    {
        public int HealthPoints;

        public int PhaseSize;

        protected override void OnRecieve(IHolder<int> holder)
        {
            HealthPoints -= holder.Item;
        }

        public override bool ShouldSwitchPhase(Boss<int> boss)
        {
            return HealthPoints < (Phases.Length - CurrentPhaseIndex) * PhaseSize;
        }

        public override void Defeated(Boss<int> boss)
        {
            Debug.Log("I'm ded");
        }
    }
}
