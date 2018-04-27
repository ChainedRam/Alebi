using ChainedRam.Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurCollisionEffect : PlayerCollisionEffect
{
    public string Name { get;set; }

    public float Duration;

    [HideInInspector]
    public BlurScreenEffect ScreenEffect;

    private void Start()
    {
        if(ScreenEffect == null)
        {
            Debug.LogError("Missing ScreenEffect refreence. " +
                "Be sure to add and setup 'BlurCollisionEffectConfig'" +
                " in ConfiguraionManager.");
        }
    }

    public override void OnHit(Player p)
    {
        p.AddEffect(ScreenEffect, Duration);

        gameObject.SetActive(false); 
    }
}
