

using ChainedRam.Core.Extentions;
using UnityEngine;

namespace ChainedRam.Core.Projection
{
    public class DelayedMotion : Motion
    {
        public Motion WrappedMotion;

        private Motion WarppedMotionCopy;

        public float Delay;

        [Header("Position and offer are not used. Only Rotation")]
        public PositionProvider LookAt; 

        private float TimeWaited;
        private GameObject Sender;
        private float Delta; 

        public override void Initialize(GameObject sender, float delta)
        {
            Sender = sender;
            Delta = delta; 
            TimeWaited = Delay;

            LookAt.SetToPosition(Direction.Center, sender.transform.position);
            if(WarppedMotionCopy != null)
            {
                Destroy(WarppedMotionCopy); 
            }
            WarppedMotionCopy = WrappedMotion.CopyTo(sender.gameObject);
            WarppedMotionCopy.enabled = false;
        }

        public override Vector2 Default()
        {
            return WarppedMotionCopy.Default(); 
        }

        public override Vector2 GetRelativeOffset(Vector2 defaultVector)
        {
            TimeWaited -= Time.deltaTime;

            if (TimeWaited > 0)
            {
                if (Sender != null && LookAt != null && LookAt.ProvidedRotation.HasValue)
                {
                    Sender.transform.eulerAngles = Vector3.forward * LookAt.ProvidedRotation.Value;
                }

                return defaultVector; 
            }
            else
            {
                if(WarppedMotionCopy.enabled == false)
                {
                    WarppedMotionCopy.enabled = true;
                    WarppedMotionCopy.Initialize(Sender, Delta);
                }

                return WarppedMotionCopy.GetRelativeOffset(defaultVector); 
            }
        }

        public override Motion CopyTo(GameObject go)
        {  
            DelayedMotion copy = go.AddComponent<DelayedMotion>();
            copy.WrappedMotion = WrappedMotion; 
            copy.Delay = Delay;
            copy.LookAt = LookAt.Copy(); 
            return copy; 
        }

        private void OnDestroy()
        {
            if (WarppedMotionCopy != null)
            {
                Destroy(WarppedMotionCopy);
            }
        }
    }
}
