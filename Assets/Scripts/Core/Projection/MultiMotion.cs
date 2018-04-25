using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Projection
{
    public sealed class MultiMotion : Motion
    {
        [Header("Motions apply from top to bottom")]
        [Header("Order Matters")]
        public Motion[] Motions;

        public override Vector2 Default()
        {
            OnValidate();
            return Motions[0].Default();
        }

        public override Vector2 GetRelativeOffset(Vector2 defaultVector)
        {
            OnValidate();
            foreach (var m in Motions)
            {
                defaultVector = m.GetRelativeOffset(defaultVector);
            }
            return defaultVector;
        }

        public override void Initialize(GameObject sender, float delta)
        {
            OnValidate();
            base.Initialize(sender, delta);
            foreach (var motion in Motions)
            {
                motion.Initialize(sender, delta);
            }
        }

        public override Motion CopyTo(GameObject go)
        {
            MultiMotion m = go.AddComponent<MultiMotion>();
            m.Motions = new Motion[Motions.Length];

            Array.Copy(Motions, m.Motions, Motions.Length);

            return m; 
        }

        #region OnValidate
        private void OnValidate()
        {
            //check list 
            if (Motions == null || Motions.Length == 0)
            {
                throw new Exception(name + " contains an empty list of motions"); //TODO create exception type
            }

            //check list elements 
            int i = 0;
            string build = "";
            foreach (var m in Motions)
            {
                if (m == null)
                {
                    build += i + ",";
                }
                i++;
            }

            if (!string.IsNullOrEmpty(build))
            {
                throw new Exception(name + $" contains null elements at {{{ build.Substring(0, build.Length - 1) }}}. Please set refrences."); //TODO create exception type
            }
        }
        #endregion
    }
}
