using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Dialog
{
    /// <summary>
    /// Simple Dialog made from string 
    /// </summary>
    public class TextDialog : Dialog
    {
        [TextArea(1, 10)]
        public string RawText;

        public DialogPauseType PauseProperty;

        [Range(0.01f, 1)]
        public float Delay;

        protected int Index;

        protected virtual string DisplayText { get { return RawText; } }

        public override DialogPauseType Property
        {
            get
            {
                return PauseProperty;
            }
        }

        public override float GetDisplayDelay()
        {
            return Delay;
        }

        public override void ResetDialog()
        {
            Index = 0;
        }

        public override void WhenDialogStart()
        {
            Index = 0;
        }

        public override IEnumerator<char> Characters()
        {
            while (Index < DisplayText.Length)
            {
                yield return DisplayText[Index++];
            }
        }
    }
}
