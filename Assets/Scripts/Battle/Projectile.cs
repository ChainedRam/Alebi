using ChainedRam.Alebi.Core;
using ChainedRam.Alebi.Interface.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A moving unit that contained a rigide body. 
/// </summary>
//TODO absract 
public class Projectile : Runnable
{
    public Rigidbody2D body;

    public override void Run()
    {
        base.Run();
    }

    public override void Stop()
    {
        base.Stop();
        gameObject.SetActive(false); 
    }
}
