using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    //TODO: Test this 
    public class SkippedTerminator : GeneratorTerminatorComponent
    {
        private bool HasSkipped;

        public override void Setup(Generator gen)
        {
            base.Setup(gen);
            HasSkipped = false;

            gen.OnSkippedEventHandler -= (s,e) =>  DidSkip();
            gen.OnSkippedEventHandler += (s, e) => DidSkip();
        }

        public override void SetApart(Generator gen)
        {
            base.SetApart(gen);
            gen.OnSkippedEventHandler -= (s, e) => DidSkip();
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
