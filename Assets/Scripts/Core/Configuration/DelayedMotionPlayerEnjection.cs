using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core;
using ChainedRam.Core.Player;

using ChainedRam.Core.Projection;
using UnityEngine;

namespace ChainedRam.Core.Configuration
{
    [RequireComponent(typeof(DelayedMotion))]
    public class DelayedMotionPlayerEnjection : Enjection
    {

        public DelayedMotion DelayedMotion;

        public override void Enject(Player.Player p)
        {
            DelayedMotion.LookAt.SetRotationFacing(p.transform);
        }

        private void OnValidate()
        {
            if (DelayedMotion == null)
            {
                DelayedMotion = GetComponent<DelayedMotion>();
            }
        }

    } 
}
