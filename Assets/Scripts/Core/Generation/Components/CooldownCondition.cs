using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    public class CooldownCondition : GeneratorConditionComponent
    {
        [Tooltip("Time to wait in seconds.")]
        [Range(0, 1000)]
        public float WaitTime;

        private float CurrentTimer;

        public override void OnGeneratorGenerated()
        {
            CurrentTimer = WaitTime;
        }

        public override bool ShouldGenerate(Generator gen)
        {
            return (CurrentTimer -= Time.deltaTime) <= 0;
        }
    }
}
