using ChainedRam.Alebi.Core;
using ChainedRam.Alebi.Interface.Battle;
using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Battle
{
    /// <summary>
    /// A Phase contains a set of waves in which a selector decides which one to run. 
    /// </summary>
    //TODO abstract -please 
    public class Phase : NestedGenerator
    {
        [Header("Phase Settings")]
        /// <summary>
        /// Selects a wave.
        /// </summary>
        public WaveSelector WaveSelector;

        /// <summary>
        /// Holds corrent running wave. TODO 'public' for debugging -KLD
        /// </summary>
        public Wave RunningWave;

        public Wave[] Waves;

        public override Generator[] ChildGenerators
        {
            get
            {
                return Waves;
            }

            set
            {
               
            }
        }

        private void Awake()
        {
            OnStartGenerating += SelectWave; 
            
        }

        public void WaveEnded()
        {
            RunningWave = null; 
        }

        public void SelectWave()
        {
            if (RunningWave != null)
            {
                RunningWave.OnStopGenerating -= WaveEnded; 
                RunningWave.StopGenerating(); 
            }

            RunningWave = WaveSelector.SelectWave(Waves, RunningWave);

            RunningWave.OnStopGenerating -= WaveEnded;
            RunningWave.OnStopGenerating += WaveEnded;

            RunningWave.StartGenerating();
        }

    }
}
