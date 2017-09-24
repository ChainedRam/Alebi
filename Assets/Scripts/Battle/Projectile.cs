using ChainedRam.Alebi.Core;
using ChainedRam.Alebi.Interface.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Battle
{
    /// <summary>
    /// A moving unit that contained a rigide body. 
    /// </summary>
    public abstract class Projectile : Runnable
    {
        public abstract void SetVelocity(Vector2 velocity);
        public abstract void SetDirection(float degrees);

        public override void Run()
        {
            base.Run();
            gameObject.SetActive(true); 
        }

        public override void Stop()
        {
            base.Stop();
            gameObject.SetActive(false);
        }
    }
}
