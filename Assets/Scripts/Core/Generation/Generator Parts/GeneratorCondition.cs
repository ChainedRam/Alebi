using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    public abstract class GeneratorCondition : MonoBehaviour
    {
        public virtual void Setup(Generator gen)
        {
            gen.OnGenerate -= OnGeneratorGenerated;
            gen.OnGenerate += OnGeneratorGenerated;
        }


        public virtual void SetApart(Generator gen)
        {
            gen.OnGenerate -= OnGeneratorGenerated;
        }

        public virtual void OnGeneratorGenerated() { }

        public abstract bool ShouldGenerate(Generator gen);
    }
}
