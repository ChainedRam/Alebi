using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Battle
{
    /// <summary>
    /// Selects a wave to be used in a phase. 
    /// </summary>
    public abstract class WaveSelector : MonoBehaviour
    {
        /// <summary>
        /// Selects next wave to be run in a phase. 
        /// </summary>
        /// <param name="waves"></param>
        /// <param name="previousWave"></param>
        /// <returns></returns>
        public abstract Wave SelectWave(Wave[] waves, Wave previousWave); 
    }
}
