using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Represnts a sequence of Dialogs masked as one dialog. 
/// </summary>
public class Conversation : Dialog
{
    /// <summary>
    /// List of Dialogs to be presented in order. 
    /// </summary>
    public Dialog[] Dialogs;

    public override DialogPauseProperty Property
    {
        get
        {
            return Dialogs[Index].Property | (Index < Dialogs.Length-1 ? DialogPauseProperty.NewPage : DialogPauseProperty.None);
        }
    }

    /// <summary>
    /// Current Dialog index
    /// </summary>
    private int Index;

    /// <summary>
    /// Indecated whether a new dialog should start 
    /// </summary>
    private bool PrepareNextDialog;

    /// <summary>
    /// Indecated whether conversation has ended
    /// </summary>
    private bool HasOnGoingDialog; 

    /// <summary>
    /// Invoked when conversation starts.  
    /// </summary>
    public UnityEvent OnStartConversation;

    /// <summary>
    /// Invoked when conversation ends .  
    /// </summary>
    public UnityEvent OnEndConversation;

    /// <summary>
    /// Wraps current dialog gettings it's DisplayDelay time 
    /// </summary>
    /// <returns></returns>
    public override float GetDisplayDelay()
    {
        return Dialogs[Index].GetDisplayDelay();
    }

    /// <summary>
    /// True unless the last Dialog doesn't HasNext
    /// </summary>
    /// <returns></returns>
    public override bool HasNext()
    {
        return !HasOnGoingDialog; // Index < Dialogs.Length-1 && Dialogs[Index].HasNext();
    }

    /// <summary>
    /// Returns next charachter from current dialog. When a dialog ends, returns a null charachter and move dialog to next. 
    /// </summary>
    /// <returns></returns>
    public override char NextCharachter()
    {
        if(PrepareNextDialog)
        {
            PrepareNextDialog = false;

            Dialogs[Index].WhenDialogEnd();
            Index++;
            if (HasNext())
            {
                Dialogs[Index].WhenDialogStart(); 
            }
            else
            {
              
            }
        }

        if(Dialogs[Index].HasNext()) 
        {
            return Dialogs[Index].NextCharachter();
        }

        //last index doesn't have next
        if (Index == Dialogs.Length - 1)
        {
            HasOnGoingDialog = true;
        }
        else
        {
            PrepareNextDialog = true;
        }
        return '\0';
    }

    /// <summary>
    /// Wraps current dialog 
    /// </summary>
    public override void ResetDialog()
    {
        foreach (Dialog d in Dialogs)
        {
            d.ResetDialog();
        }

        Index = 0;

        HasOnGoingDialog = false;
        PrepareNextDialog = false; 
    }

    /// <summary>
    /// Ends conversation and current (last) dialog. 
    /// </summary>
    public override void WhenDialogEnd()
    {
        Dialogs[Index].WhenDialogEnd();
        OnEndConversation?.Invoke();
    }

    /// <summary>
    /// Wraos pause dialog 
    /// </summary>
    public override void WhenDialogPause()
    {
        Dialogs[Index].WhenDialogPause();
    }

    /// <summary>
    /// Wraps resume dialog 
    /// </summary>
    public override void WhenDialogResume()
    {
        Dialogs[Index].WhenDialogResume();
    }

    /// <summary>
    /// Starts conversation and first dialog
    /// </summary>
    public override void WhenDialogStart()
    {
        OnStartConversation?.Invoke();
        Index = 0;
        Dialogs[Index].WhenDialogStart(); 
    }
}
