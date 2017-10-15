using ChainedRam.Alebi.Core;
using ChainedRam.Core.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHolder : MonoBehaviour, IHolder<Damage>
{


    public Damage Item
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }
}
