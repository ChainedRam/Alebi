using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    /// <summary>
    /// A terminatorgenerates based on a given number before stopping. 
    /// </summary>
    public class CountTerminator : GeneratorTerminatorComponent
    {
        #region Inspector Attributes 
        [Header("CountGenerator")]
        [Tooltip("Maximum number to generate")]
        public int MaxGeneration;
        #endregion
        #region Private Attributes 
        /// <summary>
        /// Holds counter 
        /// </summary>
        private int CurrentGeneration;

        #endregion
        #region Override GeneratorTerminator
        public override void Setup(Generator gen)
        {
            CurrentGeneration = 0;

            gen.OnGenerateEventHandler -= (s, e) => IncrementCounter();
            gen.OnGenerateEventHandler += (s, e) => IncrementCounter();

        }

        public override void SetApart(Generator gen)
        {
            gen.OnGenerateEventHandler -= (s, e) => IncrementCounter();
        }

        public override bool ShouldTerminate(Generator gen)
        {
            return CurrentGeneration >= MaxGeneration;
        }
        #endregion

        private void IncrementCounter()
        {
            CurrentGeneration++;
        }
    }
}
