
using ChainedRam.Core.Extentions; 
using UnityEngine;

namespace ChainedRam.Core.Projection
{
    public class LineMotion : AbsoluteMotion
    {
        [Header("Flat Values")]
        public float Speed = 1;

        [Range(-360, 360)]
        public float Tilt = 0; 

        [Header("Accelerated Values")]
        public float SpeedAcc = 0;
        public float TiltAcc = 0;

        public bool FaceTilt;
        public bool RelativeAngle; 

        private float TotalSpeed;
        private float TotalTilt; 
        private GameObject Proj; 

        public override void Initialize(GameObject sender, float delta)
        {
            Proj = sender;
            TotalSpeed = 0;
            TotalTilt = 0;

            if (sender != null && RelativeAngle)
            {
                TotalTilt = Proj.gameObject.transform.eulerAngles.z;
            }
        }

        public override Vector2 GetRelativeOffset(Vector2 defaultVector)
        {
            TotalSpeed += SpeedAcc;
            TotalTilt += TiltAcc; 

            defaultVector += new Vector2(0, TotalSpeed + Speed);

            return defaultVector.Rotate(TotalTilt + Tilt);
        }

        private void LateUpdate()
        {
            if(Proj != null && FaceTilt)
            {
                Proj.gameObject.transform.eulerAngles = new Vector3(0, 0, TotalTilt);
            }
        }

        public override string ToString()
        {
            return $" S:{Speed.ToString("0.00")}, SA:{SpeedAcc.ToString("0.000")}, TA:{TiltAcc.ToString("0.000")}";
        }

        public override Motion CopyTo(GameObject go)
        {
            LineMotion copy = go.AddComponent<LineMotion>();
            copy.Speed =  Speed;
            copy.Tilt = Tilt;
            copy.SpeedAcc = SpeedAcc;
            copy.TiltAcc =  TiltAcc; 
            copy.TotalSpeed = TotalSpeed;
            copy.TotalTilt = TotalTilt;
            copy.FaceTilt = FaceTilt;
            copy.RelativeAngle = RelativeAngle;
            copy.Proj = Proj; 

            return copy; 
        }
    }
}
