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
        public List<Pattern> patterns;

        public float Duration; 

        //for safty  
        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            Duration -= Time.deltaTime;

            if (Duration < 0)
            {
                Stop();
            }
        }

        /// <summary>
        /// Runs itself then all nested patterns. 
        /// </summary>
        public override void Run()
        {
            base.Run();
            gameObject.SetActive(true); 

            int i = 0; 

            //TODO position each pattern 
            foreach (Pattern p in patterns)
            {
                SetUpPattern(i++, p); 
                p.Run();
            }
        }

        public abstract void SetUpPattern(int index, Pattern pattern); 
       

        /// <summary>
        /// Stops all nested pattern then itself. 
        /// </summary>
        public override void Stop()
        {
            foreach (Pattern p in patterns)
            {
                p.Stop(); 
            }
            base.Stop();
        }
    }
}
