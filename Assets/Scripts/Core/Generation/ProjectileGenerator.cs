using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainedRam.Core.Configuration; 


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
            ConfigureManager.ConfigureGameObject(generated.gameObject);
            generated.Setup(Delta);
        }
    }
}