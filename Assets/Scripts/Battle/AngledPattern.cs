﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Battle
{
    public class AngledPattern : Pattern
    {
        [Header("AngledPattern")]
        [Range(-180, 180)]
        public float Angle;

        public float LinearSpeed; 

        /// <summary>
        /// Normalizes angle to look down 
        /// </summary>
        private const float AlphaAngle = -135;

        /// <summary>
        /// Creates and projects a new projectile. 
        /// </summary>
        public override void Project(Projectile p)
        {
            p.transform.parent = transform; 

            p.transform.localPosition = Vector2.zero;
            p.transform.localRotation = Quaternion.identity;


            p.SetVelocity(Vector2.one.Rotate(AlphaAngle + Angle) * LinearSpeed * (Acceleration));
            p.SetDirection(AlphaAngle + Angle);
        }
    }
}