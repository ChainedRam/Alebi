using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class UnityEventGenerator : OnceGenerator
{
    public UnityEvent OnGenerateCalled;

    protected override void OnGenerate(GenerateEventArgs e)
    {
        OnGenerateCalled.Invoke(); 
    }

}
