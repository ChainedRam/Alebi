using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Represent a dialog box reach from a text file.
/// NOTE: this is a customed class and by no means a standard! Feel free to make your own Dialog child. 
/// </summary>
public class DialogSegment : Dialog
{
    public TextAsset textFile;

    [Range(0f,.5f)]
    public float CCharachterDelay; 

    [Range(0f, .5f)]
    public float NewLineDelay; 

    public DialogPauseProperty property;

    private int CharachterIndex;
    protected string text;

    public UnityEvent OnStartDialog;
    public UnityEvent OnEndDialog;

    public override DialogPauseProperty Property
    {
        get
        {
            return property;
        }
    }

    protected virtual void Start()
    {
        text = textFile.text; 
        ResetDialog(); 
    }

    public override void ResetDialog()
    {
        CharachterIndex = 0;
    }

    public override char NextCharachter()
    {
        return text[CharachterIndex++]; 
    }

    public override bool HasNext()
    {
        return CharachterIndex < text.Length; 
    }

    public override void WhenDialogStart()
    {
        OnStartDialog?.Invoke();
    }
    public override void WhenDialogEnd()
    {
        OnEndDialog?.Invoke();
    }
    public override void WhenDialogPause()
    {

    }
    public override void WhenDialogResume()
    {

    }

    public override float GetDisplayDelay()
    {
        if (text[CharachterIndex-1] == '\n')
        {
            return NewLineDelay; 
        }

        return CCharachterDelay; 
    }
    
}

