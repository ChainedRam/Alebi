using ChainedRam.Alebi.Core;
using ChainedRam.Alebi.Interface.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Battle
{
    /// <summary>
    /// A Wave Runs a set of pattern under it. 
    /// </summary>
    public abstract class Wave : Runnable
    {
        /// <summary>
        /// Set of patterns to run. 
        /// </summary>
        public Pattern[] Patterns;

        public WaveTerminator Terminator; 

        /// <summary>
        /// Runs itself then all nested patterns. 
        /// </summary>
        public override void Run()
        {
            base.Run();
            gameObject.SetActive(true);
            int i = 0; 

            foreach (Pattern p in Patterns)
            {
                SetUpPatternPosition(i++, p);

                p.Run();
                
                //add events. Events are cleared when pattern is run.  
                p.OnProjectileLaunched += (pro) => Terminator.OnProjectileLaunched(p, pro);

            }

            Terminator.Setup(Patterns);
        }

        public abstract void SetUpPatternPosition(int index, Pattern pattern); 

        /// <summary>
        /// Stops all nested pattern then itself. 
        /// </summary>
        public override void Stop()
        {
            foreach (Pattern p in Patterns)
            {
                p.Stop(); 
            }
            base.Stop();

            gameObject.SetActive(false); //hide wave object 
        }
    }
}
