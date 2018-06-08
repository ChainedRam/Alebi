using ChainedRam.Core.Dialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChainedRam.Core.Dialog
{
    public class ButtionEmotionGenerator : MonoBehaviour
    {
        public AnimationalCharacter Character;

        public Button ButtonPrefab;
        public GameObject ButtonParent;

        private string HoldCharacter = "";

        private void Start()
        {
            foreach (var clip in Character.clips)
            {
                string emotionName = clip.name.Substring(clip.name.LastIndexOf('_') + 1); //WRONG 

                Button plsAddMe = Instantiate(ButtonPrefab, ButtonParent.transform);
                plsAddMe.GetComponentInChildren<Text>().text = emotionName;
                plsAddMe.onClick.AddListener(() => { Character.SetEmotion(emotionName.ToLower() + HoldCharacter); });
                plsAddMe.gameObject.SetActive(true);
            }
        }

        public void TurnHoldOn()
        {
            HoldCharacter = "+";
        }

        public void TurnHoldOff()
        {
            HoldCharacter = "";
            Character.Release();
        }
    }
}
