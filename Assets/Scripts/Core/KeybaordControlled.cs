using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Player
{
    /// <summary>
    /// Allows Object to  move using keybaord.
    /// </summary>
    public class KeybaordControlled : MonoBehaviour
    {
        #region Inspecter Variables
        /// <summary>
        /// Speed in which the object will move. 
        /// </summary>
        public float Speed;
        #endregion
        #region Private Variables
        //my body is ready, do not inheret
        new private Rigidbody2D rigidbody2D;

        /// <summary>
        /// Holds key names and their direction values. 
        /// </summary>
        private Dictionary<string, Vector2> RegisteredKeys; //can improve to be displayable in inspecter.  
        #endregion
        #region Unity Methods
        private void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();

            RegisteredKeys = new Dictionary<string, Vector2>()
        {
            { "up", Vector2.up },
            { "down", Vector2.down },
            { "right", Vector2.right },
            { "left", Vector2.left }
        };
        }


        /// <summary>
        /// Move up or left right 
        /// </summary>
        void FixedUpdate()
        {
            foreach (var pair in RegisteredKeys)
            {
                if (Input.GetKey(pair.Key))
                {
                    rigidbody2D.velocity += (pair.Value * Speed * Time.deltaTime);
                }
            }
        }
        #endregion
    }
}
