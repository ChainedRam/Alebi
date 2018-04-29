using ChainedRam.Core.Dialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogFontBuilder : MonoBehaviour
{
    public Text LetterTemplate;
    public DialogFont DialogFont; 

    private void Start()
    {
        for (char c = 'A' ; c < 'z'; c++)
        {
           var text =  Instantiate(LetterTemplate, DialogFont.transform);

            text.text = "" + c;
            text.name = "" + c;
            var letter = text.gameObject.AddComponent<Letter>();

            letter.Type = (LetterType)c;

            DialogFont.Letters.Add(letter); 
        }
    }

}
