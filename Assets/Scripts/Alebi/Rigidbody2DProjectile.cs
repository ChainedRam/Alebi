using ChainedRam.Core.Extentions;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Rigidbody2DProjectile : Projectile
{
    public Rigidbody2D body;


    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Pixel per second 
    /// </summary>
    public float Speed;

    [Range(-90,90)]
    public float Angle = 0;

    private void FixedUpdate()
    {
        
    }

    private void OnValidate()
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }

        if(body == null)
        {
            Debug.LogError("Missing Rigidbody2D");
        }
    }
}

