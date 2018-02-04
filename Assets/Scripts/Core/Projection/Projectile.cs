using UnityEngine;
using ChainedRam.Core.Extentions;

namespace ChainedRam.Core.Projection
{
    /// <summary>
    /// A moving unit that contained a rigide body. 
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        public Motion Motion;

        private void Start()
        {
            Motion.Initialize(); 
        }

        public virtual void Setup(float delta)
        {
            Motion.Initialize(delta); 
        }

        public void FixedUpdate()
        {
            Vector2 offset = Motion.GetOffset(); 

            transform.position += (Vector3)offset;
        }
    }
}
