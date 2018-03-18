using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainedRam.Core.Projection;

namespace ChainedRam.Core.Generation
{
    public class PlayerTarggettedProjectileGenerator : TargettedProjectileGenerator
    {
        [ContextMenu("Copy From Parent")]
        private void CopyFromTarget()
        {
            TargettedProjectileGenerator t = GetComponent<TargettedProjectileGenerator>();

            GenerateAt = t.GenerateAt;
            Target = t.Target;
            WaitTime = t.WaitTime;
            this.Prefab = t.Prefab;
            Parent = t.Parent;
            MotionOverride = t.MotionOverride;
        }
    }

}