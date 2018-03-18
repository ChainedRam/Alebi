using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ChainedRam.Core.Dialog
{

    public class FakeEmotionalCharachter : EmotionalCharacter
    {
        public Text label;

        public EmotionalCharacter Face;

        private void Start()
        {
            //ignore parent's Start()
        }

        public override void SetEmotion(string name)
        {
            label.text = name;
            Face?.SetEmotion(name);

        }
    }
}
