using ChainedRam.Alebi.Battle;
using ChainedRam.Alebi.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Core
{
    /// <summary>
    /// Implements IDamage with integer as a damage type. 
    /// </summary>
    public class DamageComponent : MonoBehaviour, IDamage<int>
    {
        public int DamagePower;

        int IDamage<int>.Damage
        {
            get
            {
                return DamagePower;
            }
        }
    }
}

