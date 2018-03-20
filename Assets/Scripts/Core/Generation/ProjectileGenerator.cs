using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ChainedRam.Core.Generation
{
    public class ProjectileGenerator : QueueInstanceGenerator<Projectile>
    {
        [Header("Projectile Generator")]
        public ChainedRam.Core.Projection.Motion MotionOverride;

        public override void SetupGenerated(Projectile generated)
        {
            base.SetupGenerated(generated);
            generated.Motion = MotionOverride ?? generated.Motion;

            generated.Setup(Delta);
        }
    }
}