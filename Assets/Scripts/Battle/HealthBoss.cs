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
    public class BossHealth : DamageCollider2DReciver
    {
        public int HealthPoints;

        public int PhaseSize;

        public bool ShouldSwitchPhase()
        {
            return true; // HealthPoints < (Phases.Length - CurrentPhaseIndex) * PhaseSize;
        }

        public void Defeated()
        {
            Debug.Log("I'm ded");
        }
    }
}
