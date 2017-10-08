using ChainedRam.Alebi.Core;
using ChainedRam.Alebi.Interface.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Battle
{
    /// <summary>
    /// A Phase contains a set of waves in which a selector decides which one to run. 
    /// </summary>
    //TODO abstract 
    public class Phase : Runnable
    {
        /// <summary>
        /// Waves to select from. 
        /// </summary>
        public Wave[] Waves;

        public float PhaseCoolDown;

        public float CurrentCoolDown; 

        /// <summary>
        /// Selects a wave. TODO not implemented -KLD
        /// </summary>
        public WaveSelector WaveSelector;

        /// <summary>
        /// Holds corrent running wave. TODO 'public' for debugging -KLD
        /// </summary>
        public Wave RunningWave;


        public void WaveEnded()
        {
            RunningWave = null; 
            CurrentCoolDown = PhaseCoolDown;
        }


        private void Update()
        {
            if(RunningWave != null)
            {
                return; 
            }

            CurrentCoolDown -= Time.deltaTime; 

            if(CurrentCoolDown <= 0)
            {
                SelectWave(); 
            }
        }

        public void SelectWave()
        {
            RunningWave = WaveSelector.SelectWave(Waves, RunningWave);

            RunningWave.OnStop -= WaveEnded;
            RunningWave.OnStop += WaveEnded;

            RunningWave.Run();
        }

        /// <summary>
        /// Selects and runs a wave while stopping previous wave. 
        /// </summary>
        public override void Run()
        {
            base.Run();

            WaveEnded(); 
        }

        /// <summary>
        /// Stops current wave. 
        /// </summary>
        public override void Stop()
        {
            base.Stop();
            RunningWave.Stop();
        }
    }
}
