using ChainedRam.Alebi.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows objects to recieve Damage 
/// </summary>
public class DamageReciever : Collider2DReciever<DamagePickable>
{
    //Used for demonstration. TODO remove 
    public int HP; 

    public override void OnReceive(DamagePickable item)
    {
        base.OnReceive(item);

        HP -= item.Value; 
    }
}
