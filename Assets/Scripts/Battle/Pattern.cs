using ChainedRam.Alebi.Core;
using ChainedRam.Alebi.Interface.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fires a projectile in a pattenr 
/// </summary>
//TODO abstract 
public class Pattern : Runnable
{
    public bool Projecting; 
    public Vector2 SpawnPosition;

    public Projectile projectileType;

    //TODO move to linear projectile 
    public float Speed;

    //TODO move to linear projectile 
    public float Angle;

    private List<Projectile> FiredProjectiles;


    //TODO convert to units per second or make a class. Reloader 
    [Range(0, 5)]
    public float FireRate;

    private float currentTime;
    
    // Use this for initialization
    void Start()
    {
        Projecting = false; 
        currentTime = 0f;
        FiredProjectiles = new List<Projectile>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Projecting == false)
        {
            ResetLaunch(); 
            return; 
        }

        if ((currentTime += Time.deltaTime) > FireRate)
        {
            ResetLaunch(); 
            Project();
        }
    }

    /// <summary>
    /// Creates and projects a new projectile. 
    /// </summary>
    public void Project()
    {
        Projectile p = Instantiate(projectileType, SpawnPosition, Quaternion.identity, transform);

        FiredProjectiles.Add(p);

        p.body.velocity = new Vector2(Speed, -2f); //Speed, Angle
    }

    /// <summary>
    /// resets timer to creating projectiles. TODO move to Reloader class. 
    /// </summary>
    public void ResetLaunch()
    {
        currentTime = 0;
    }

    /// <summary>
    /// Enables creating projectiles. 
    /// </summary>
    public override void Run()
    {
        base.Run();
        Projecting = true;
    }

    /// <summary>
    /// Stops projecting and destroy all created projectiles. -TODO queue and reload. 
    /// </summary>
    public override void Stop()
    {
        base.Stop();
        Projecting = false;
        //delete and stop all projectiles 
        for (int i = 0; i < FiredProjectiles.Count; i++)
        {
            Destroy(FiredProjectiles[i].gameObject); 
        }

        FiredProjectiles.Clear(); 
    }
}
