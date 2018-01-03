using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    //TODO: Test this 
    public class SkippedTerminator : GeneratorTerminator
    {
        private bool HasSkipped;

        public override void Setup(Generator gen)
        {
            base.Setup(gen);
            HasSkipped = false;

            gen.OnSkippedGenerate -= DidSkip;
            gen.OnSkippedGenerate += DidSkip;
        }

        public override void SetApart(Generator gen)
        {
            base.SetApart(gen);
            gen.OnSkippedGenerate -= DidSkip;
        }

        public override bool ShouldTerminate(Generator gen)
        {
            return HasSkipped;
        }

        private void DidSkip()
        {
            HasSkipped = true;
        }
    }
}
