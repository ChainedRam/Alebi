using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSegment : MonoBehaviour
{
    public TextAsset textFile;

    [Range(0f,.5f)]
    public float CharachterDisplayTime;

    [Range(0f, .5f)]
    public float LineDisplayTime;

    public DialogProperty property;

    private int CharachterIndex;
    private string text; 


    private void Start()
    {
        text = textFile.text; 
        ResetDialog(); 
    }

    public void ResetDialog()
    {
        CharachterIndex = 0;
    }

    public char NextCharachter()
    {
        return text[CharachterIndex++]; 
    }

    public bool HasNext()
    {
        return CharachterIndex < text.Length; 
    }
}

