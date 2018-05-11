using System;
using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Generation;
using ChainedRam.Core.Projection;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    [Obsolete("Tragetting has been moved into DelayedMotion")]
    public class TargettedProjectileGenerator : ProjectileGenerator
    {
        [Header("Targetted Options")]
        public PositionProvider Target;

        //private bool IsMoving;
        private Projectile Projectile;

        protected override void OnGenerate(GenerateEventArgs e)
        {
            base.OnGenerate(e);
            //StartCoroutine(StareAtPlayer(WaitTime));
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            //if (IsMoving == true)
            //{
            //    Projectile.transform.up = (Target.ProvidedPosition - Projectile.transform.position);
            //}
        }

       /* private IEnumerator StareAtPlayer(float prepTime)
        {
            IsMoving = true;
            yield return new WaitForSeconds(prepTime);

            Projectile.Setup(Delta); 
            IsMoving = false;
        }*/

        //protected override Projectile CreateInstance()
        //{
        //    Projectile = base.CreateInstance();
        //    Projectile.enabled = false;

        //    return Projectile;
        //}

        /*public override void SetupGenerated(Projectile generated)
        {
            base.SetupGenerated(generated);

            DelayedMotion delayedMotion = Projectile.gameObject.AddComponent<DelayedMotion>();


            delayedMotion.Delay = WaitTime;
            delayedMotion.Warpped = MotionOverride ?? Projectile.Motion;

            Projectile.Motion = delayedMotion;
            Projectile.enabled = true;
        }*/
    }

}

