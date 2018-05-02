using ChainedRam.Core.Dialog;
using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogGenerator : Generator
{
    public Dialog Dialog;
    public DialogDisplayer Displayer; 

    protected override void OnGenerate(GenerateEventArgs e)
    {
        Dialog.OnEnd += End; 
        Dialog.OnEnd += () => Dialog.OnEnd -= End;
        Displayer.StartDialog(Dialog);
    }

    protected override bool ShouldGenerate()
    {
        return ShouldGenerateOnce(); 
    }

    protected override bool ShouldTerminate()
    {
        return false; 
    }
}
