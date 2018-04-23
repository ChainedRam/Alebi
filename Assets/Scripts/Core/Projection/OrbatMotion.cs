using ChainedRam.Core.Extentions;
using UnityEngine;

namespace ChainedRam.Core.Projection
{
    public class OrbatMotion : AbsoluteMotion
    {
        public float Radius;
        public float AngularVelocity;
        public float StartingAngle;


        private float TotalAngle;

        private float DegreeRadSize;

        public override void Initialize(GameObject sender, float delta = 1)
        {
            base.Initialize(sender, delta);
            TotalAngle = StartingAngle;

            DegreeRadSize = AngularVelocity * ((2 * Mathf.PI * Radius) / 360);
        }

        public override Vector2 GetRelativeOffset(Vector2 defaultVector)
        {
            TotalAngle += AngularVelocity;

            var o = (DegreeRadSize * Vector2.up).Rotate(TotalAngle);


            return defaultVector + o;
        }
    }
}
