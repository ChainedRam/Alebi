using ChainedRam.Alebi.Battle;
using ChainedRam.Core.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IHolder<Weapon>
{
    public Weapon Item
    {
        get
        {
            return this; 
        }
    }

    public Projectile proj; 

   

   
}
