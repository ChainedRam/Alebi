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
        public List<Wave> Waves;

        /// <summary>
        /// Selects a wave. TODO not implemented -KLD
        /// </summary>
        public WaveSelector WaveSelector;

        /// <summary>
        /// Holds corrent running wave. TODO 'public' for debugging -KLD
        /// </summary>
        public Wave RunningWave;

        //Debugging use 
        public void StartWaveAtIndex(int index)
        {
            /*if(RunningWave != null)
            {
                RunningWave.Stop(); 
            }*/

            RunningWave = Waves[index];

            //for testing 
            Run();
        }

        //Debug 
        public void FireNextWave()
        {
            StartWaveAtIndex(0);//Random.Range(0, 2));  
        }

        /// <summary>
        /// Selects and runs a wave while stopping previous wave. 
        /// </summary>
        public override void Run()
        {
            base.Run();

            //TODO add selection logic here 

            RunningWave.OnStop -= FireNextWave;
            RunningWave.OnStop += FireNextWave;

            //TODO Wave doesn't have durations. 
            RunningWave.Duration = 50;
            RunningWave.Run();
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
