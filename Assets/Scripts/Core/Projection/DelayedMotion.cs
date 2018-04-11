

using UnityEngine;

namespace ChainedRam.Core.Projection
{
    public class DelayedMotion : Motion
    {
        public Motion Warpped;

        public float Delay;

        private float TimeWaited;

        private GameObject sender;
        private float delta; 

        public override void Initialize(GameObject sender, float delta)
        {
            this.sender = sender;
            this.delta = delta; 
            TimeWaited = Delay;

            Warpped.enabled = false; 
        }

        public override Vector2 Default()
        {
            return Warpped.Default(); 
        }

        public override Vector2 GetRelativeOffset(Vector2 defaultVector)
        {
            if(TimeWaited > 0)
            {
                return defaultVector; 
            }
            else
            {
                if(Warpped.enabled == false)
                {
                    Warpped.enabled = true;
                    Warpped.Initialize(sender, delta);
                }

                return Warpped.GetRelativeOffset(defaultVector); 
            }
        }

        void Awake()
        {
            TimeWaited = Delay; 
        }

        void Update()
        {
            TimeWaited -= Time.deltaTime; 
        }
    }
}
