using ChainedRam.Alebi.Interface;
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

        public override void RecieveDamage(IDamage<int> dmg)
        {
            HealthPoints -= dmg.Damage;
        }

        public override bool ShouldSwitchPhase(Boss<int> boss)
        {
            return HealthPoints < (Phases.Length - CurrentPhaseIndex) * PhaseSize;
        }

    }
}
