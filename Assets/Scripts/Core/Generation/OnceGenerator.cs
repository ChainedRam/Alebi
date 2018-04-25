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
        protected override bool ShouldGenerate()
        {
            return ShouldGenerateOnce(); 
        }

        protected override bool ShouldTerminate()
        {
            return ShouldTerminateOnSkippedGeneration(); 
        }
    }
}
