using ChainedRam.Alebi.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Recieves dynamic object to set the target pickable type 
/// </summary>
public class DynamicReciever : Collider2DReciever<Pickable>
{
    public Pickable PickableType;

    public override void OnReceive(Pickable item)
    {
       
    }

    protected override Type GetTargetPickableType()
    {
        return PickableType.GetType(); 
    }

}
