using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Dialog
{
    public class Letter : MonoBehaviour
    {
        /// <summary>
        /// Holds letter type 
        /// </summary>
        public LetterType Type;

        /// <summary>
        /// Shows letter by activating game object. 
        /// </summary>
        public virtual void Appear()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides letter by deactivating game object. 
        /// </summary>
        public virtual void Dissapear()
        {
            gameObject.SetActive(false);
        }
    }
}
