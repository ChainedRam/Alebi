using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ChainedRam.Core.Dialog
{

    public class FakeEmotionalCharachter : EmotionalCharacter
    {
        public Text label;
        public GameObject Fade; 


        [Header("Optional")]
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

        public override void Darken()
        {
            Fade.SetActive(true); 
        }

        public override void Undarken()
        {
            Fade.SetActive(false);
        }
    }
}
