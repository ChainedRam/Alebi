using ChainedRam.Alebi.Core;
using ChainedRam.Core.Collider;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainedRam.Core.Interface;

public class WeaponCollector : Collider2DReciever<Weapon> {

    public override void Recieve(IHolder<Weapon> holder)
    {
        Debug.Log(holder.Item.name + " was recieved.");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
