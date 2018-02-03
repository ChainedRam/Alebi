using UnityEngine;
using ChainedRam.Core.Extentions;

namespace ChainedRam.Core.Projection
{
    /// <summary>
    /// A moving unit that contained a rigide body. 
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        public Motion motion;


        private void Start()
        {
            motion.Initialize();
        }
        public virtual void Setup(float delta)
        {
            
        }

        public void FixedUpdate()
        {

            Vector2 offset = motion.GetOffset(); 

            transform.position += (Vector3)offset;
        }

       


    }
}
