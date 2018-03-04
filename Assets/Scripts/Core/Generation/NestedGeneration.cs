using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Generation;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    public class NestedGeneration : WaitGeneration
    {
        public TimedGeneration[] Generations;

        public override void Trigger(Generator sender)
        {
            foreach (TimedGeneration g in Generations)
            {
                g.Trigger(sender);
            }
        }

        protected override void SyncWaitTime()
        {
            float numberStore = Generations[0].Seconds;
            foreach (var item in Generations)
            {
                if (numberStore < item.Seconds)
                {
                    numberStore = item.Seconds;
                }
            }
            WaitTime = numberStore;
        }
    }
}
