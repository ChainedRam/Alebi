using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    public abstract class GeneratorTerminatorComponent : MonoBehaviour
    {
        public virtual void Setup(Generator gen) { }
        public virtual void SetApart(Generator gen) { }

        public abstract bool ShouldTerminate(Generator gen);
    }
}
