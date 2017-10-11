using ChainedRam.Alebi.Battle;
using ChainedRam.Alebi.Interface;
using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Interface;
using UnityEngine;

namespace ChainedRam.Alebi.Core
{
    /// <summary>
    /// Implements IDamage with integer as a damage type. 
    /// </summary>
    public class DamageComponent : MonoBehaviour, IHolder<int>
    {
        public int DamagePower;

        public int Item
        {
            get
            {
                return DamagePower;
            }
        }
    }
}

