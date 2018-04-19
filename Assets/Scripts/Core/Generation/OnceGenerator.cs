using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    /// <summary>
    /// Generates once. ONCE! 
    /// </summary>
    public abstract class OnceGenerator : Generator
    {
        protected sealed override bool ShouldGenerate()
        {
            return ShouldGenerateOnce(); 
        }

        protected sealed override bool ShouldTerminate()
        {
            return ShouldTerminateOnSkippedGeneration(); 
        }
    }
}
