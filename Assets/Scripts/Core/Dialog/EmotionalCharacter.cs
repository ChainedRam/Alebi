using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChainedRam.Core.Dialog
{
    /// <summary>
    /// For emotionful dialogs. TODO: please change to an event listener -what? 
    /// </summary>
    public abstract class EmotionalCharacter : MonoBehaviour
    {
        public abstract void SetEmotion(string emotionName);
       
        public abstract void Darken();

        public abstract void Undarken();
       
    }
}
