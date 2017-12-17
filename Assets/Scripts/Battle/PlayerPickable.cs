using ChainedRam.Core.Interface;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents items that can be picked by <see cref="PlayerReciever"/>
/// </summary>
public class PlayerPickable : Pickable
{
    public override bool CanBePicked(GameObject PickedBy)
    {
        return true;
    }

    public override void OnPicked(GameObject PickedBy)
    {

    }
}