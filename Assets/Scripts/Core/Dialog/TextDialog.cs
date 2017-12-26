using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple Dialog made from string 
/// </summary>
public class TextDialog : Dialog
{
    [TextArea(1,10)]
    public string Text;

    public DialogPauseProperty PauseProperty;

    [Range(0.01f, 1)]
    public float Delay;

    protected int Index; 
    

    public override DialogPauseProperty Property
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

    public override bool HasNext()
    {
        return Index < Text.Length; 
    }

    public override char NextCharachter()
    {
        return Text[Index++]; 
    }

    public override void ResetDialog()
    {
        Index = 0; 
    }

    public override void WhenDialogStart()
    {
        Index = 0; 
    }

}
