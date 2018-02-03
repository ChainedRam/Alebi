using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace ChainedRam.Core.Dialog
{
    /// <summary>
    /// Parses text to extra emotion that will trigger at their mentioned time. 
    /// </summary>
    public class EmotionalDialog : TextDialog
    {
        public EmotionalCharacter Charachter;

        public List<IndexedEmotion> EmotionList;
        private int EmotionIndex;

        public UnityEvent OnStart;
        public UnityEvent OnEnd;

        private string NonEmotionalText;


        protected override string DisplayText => NonEmotionalText;


        /// <summary>
        /// Parses text on run time.  //Think of a better way. No need to recompute this 
        /// </summary>
        void Start()
        {
            EmotionList = new List<IndexedEmotion>();
            Parse();
        }

        private void Parse()
        {
            EmotionList = GetComponentsInChildren<IndexedEmotion>().ToList();

            foreach (IndexedEmotion emo in EmotionList)
            {
                Destroy(emo);
                Destroy(emo.gameObject);
            }
            EmotionList = new List<IndexedEmotion>();

            EmotionIndex = 0;
            //parse text 
            bool start = false;

            string result = "";
            string emotionBuild = "";

            for (int i = 0; i < RawText.Length; i++)
            {
                char c = RawText[i];

                switch (c)
                {
                    case '<':
                        emotionBuild = "";
                        start = true;
                        break;
                    case '>':
                        if (start == false)
                        {
                            goto default;  //i'm sorry -.-
                        }

                        IndexedEmotion newEmotion = new GameObject(result.Length + " " + emotionBuild).AddComponent<IndexedEmotion>();
                        newEmotion.transform.SetParent(transform);

                        EmotionList.Add(newEmotion.Init(result.Length, emotionBuild));
                        start = false;
                        break;
                    default:
                        if (start)
                        {
                            emotionBuild += "" + c;
                        }
                        else
                        {
                            result += "" + c;
                        }
                        break;
                }
            }

            NonEmotionalText = result.Trim();
        }

        /// <summary>
        /// Trigger emotion when time is ready 
        /// </summary>
        /// <returns></returns>
        public override char NextCharachter()
        {
            if (EmotionIndex < EmotionList.Count && Index == EmotionList[EmotionIndex].Index)
            {
                Charachter?.SetEmotion(EmotionList[EmotionIndex].Emotion);
                EmotionIndex++;
            }

            return base.NextCharachter();
        }

        public override void WhenDialogResume()
        {
            base.WhenDialogResume();
        }

        public override void ResetDialog()
        {
            base.ResetDialog();
            EmotionIndex = 0;
        }

        public override void WhenDialogStart()
        {
            base.WhenDialogStart();

            EmotionIndex = 0;

            OnStart?.Invoke();
        }

        public override void WhenDialogEnd()
        {
            base.WhenDialogEnd();
            OnEnd?.Invoke();
        }
    }
}

