using ChainedRam.Core.Dialog.Extention;
using ChainedRam.Core.Enums.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChainedRam.Core.Dialog
{
    public class DialogBox : DialogDisplayer
    {
        #region Public Attributes 
        public Text TextBox;
        public GameObject PauseIndecator; //TODO get by finding + set to private

        protected override void AppendLetter(char next)
        {
            TextBox.text += "" + next; 
        }

        protected override void ClearText()
        {
            TextBox.text = string.Empty; 
        }
        #endregion

        protected override void OnPause()
        {
            PauseIndecator.SetActive(true); 
        }

        protected override void OnResume()
        {
            PauseIndecator.SetActive(false);
        }

    }
}
