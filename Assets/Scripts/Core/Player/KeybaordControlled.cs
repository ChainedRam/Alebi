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
        private float CoolDown;
        private Vector2? Destination;
        [Range(0, 2)]
        public float CoolDownDuration = 1;
        private const float DistanceThreshold = 0.01f;

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
            /*
            if (Destination != null)
            {
                //
                transform.position = Vector2.MoveTowards(transform.position, (Vector2)Destination, Speed);
                
                if (Vector2.Distance(transform.position,(Vector2)Destination)<DistanceThreshold)
                {
                    transform.position = (Vector2)Destination;
                    Destination = null;
                    CoolDown = 0;
                }

                
            }
            */

            CoolDown += Time.fixedDeltaTime;
            if (CoolDown < CoolDownDuration)
            {
                return;
            }

            if (Input.GetKey("up"))
            {
                Debug.Log("up");
                CoolDown = 0;
                rigidbody2D.velocity =  Vector2.up * Speed*Time.fixedDeltaTime;
            }
            else if (Input.GetKey("down"))
            {
                Debug.Log("down");
                CoolDown = 0;
                rigidbody2D.velocity = Vector2.down * Speed*Time.fixedDeltaTime;
            }
            else if (Input.GetKey("right"))
            {
                Debug.Log("right");
                CoolDown = 0;
                rigidbody2D.velocity = Vector2.right * Speed*Time.fixedDeltaTime;
            }
            else if (Input.GetKey("left"))
            {
                Debug.Log("left");
                CoolDown = 0;
                rigidbody2D.velocity = Vector2.left * Speed*Time.fixedDeltaTime;
            }

            else
            {
                rigidbody2D.velocity = Vector2.zero;
            }


            /*
            foreach (var pair in RegisteredKeys)
            {
                if (Input.GetKey(pair.Key))
                {
                    rigidbody2D.velocity += (pair.Value * Speed * Time.deltaTime);
                }
            }
            */
        }
        #endregion
    }
}
