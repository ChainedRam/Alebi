using ChainedRam.Alebi.Core;
using ChainedRam.Alebi.Interface.Battle;
using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Battle
{
    /// <summary>
    /// A Wave Runs a set of pattern under it. 
    /// </summary>
    public abstract class Wave : NestedGenerator<Pattern>
    {
        protected override void WhenBegin()
        {
            base.WhenBegin();
            Setup(); 
        }

        /// <summary>
        /// Runs itself then all nested patterns. 
        /// </summary>
        public void Setup()
        {
            int i = 0;

            foreach (Pattern p in Children)
            {
                SetUpPatternPosition(i++, p);
            }
        }

        public abstract void SetUpPatternPosition(int index, Pattern pattern); 

    }
}
