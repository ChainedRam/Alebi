using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    /// <summary>
    /// Wrap a generater and stops after a given time. 
    /// </summary>
    public class DurationTerminator : GeneratorTerminatorComponent
    {
        #region Inspector Attributes
        [Header("TimedGeneratorContainer")]

        [Tooltip("In seconds")]
        public float Duration;
        #endregion
        #region Private Attributes
        public float CurrentTime;
        #endregion
        #region Override GeneratorTerminator
        public override void Setup(Generator gen)
        {
            CurrentTime = 0;
        }

        public override bool ShouldTerminate(Generator gen)
        {
            return ((CurrentTime += Time.deltaTime) >= Duration);
        }
        #endregion
    }
}
