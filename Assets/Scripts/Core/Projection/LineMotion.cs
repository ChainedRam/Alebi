
using ChainedRam.Core.Extentions;

using UnityEngine;

namespace ChainedRam.Core.Projection
{
    public class LineMotion : AbsoluteMotion
    {
        [Header("Flat Values")]
        public float Speed = 1;
        [Range(-180, 180)]
        public float Tilt = 0;

        [Header("Accelerated Values")]
        public float SpeedAcc = 0;
        public float TiltAcc = 0;

        private float TotalSpeed;
        private float TotalTilt;

        public override void Initialize(float delta)
        {
            TotalTilt = 0;
            TotalSpeed = 0;
        }

        public override Vector2 GetRelativeOffset(Vector2 defaultVector)
        {
            TotalSpeed += SpeedAcc;
            TotalTilt += TiltAcc;

            defaultVector += new Vector2(0, TotalSpeed + Speed);

            return defaultVector.Rotate(-(Tilt + TotalTilt));
        }

        public override string ToString()
        {
            return $" S:{Speed.ToString("0.00")}, T:{Tilt.ToString("00.0")}, SA:{SpeedAcc.ToString("0.000")}, TA:{TiltAcc.ToString("0.000")}";
        }

    }
}
