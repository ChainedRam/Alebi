using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    public abstract class GeneratorConditionComponent : MonoBehaviour
    {
        public virtual void Setup(Generator gen)
        {
            gen.OnGenerateEventHandler -= (s, e) => OnGeneratorGenerated();
            gen.OnGenerateEventHandler += (s, e) => OnGeneratorGenerated();
        }


        public virtual void SetApart(Generator gen)
        {
            gen.OnGenerateEventHandler -= (s, e) => OnGeneratorGenerated();
        }

        public virtual void OnGeneratorGenerated() { }

        public abstract bool ShouldGenerate(Generator gen);
    }
}
