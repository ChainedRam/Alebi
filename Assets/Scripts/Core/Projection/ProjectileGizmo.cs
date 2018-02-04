﻿using ChainedRam.Core.Projection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGizmo : MonoBehaviour
{
    public Projectile projectile;

    [Range(10, 100)]
    public float MaxLineLength;

    [Range(200, 2000)]
    public int MaxLineDrawn; 

    private void Start()
    {
        projectile = null;
    }

    private void OnDrawGizmos()
    {
        if (projectile == null)
        {
            return;
        }

        if (projectile.Motion == null)
        {
            throw new NullReferenceException(projectile.name + "'s motion is null");
        }

        Motion motion = projectile.Motion;
        motion.Initialize();
       
        Color[] c = { Color.white, Color.green, Color.blue, Color.red, Color.green };
        Color prev = Gizmos.color;

        Vector3 to, from = projectile.transform.position;
        int i;
        float lineLength;

        for (i = 0, lineLength = 0; lineLength <= MaxLineLength && i < MaxLineDrawn; i++, from = to, lineLength += Vector2.Distance(from, to))
        {
            Gizmos.color = c[i % c.Length];
            to = from + (Vector3)motion.GetOffset();
            Gizmos.DrawLine(from, to);
        }
        Gizmos.color = prev; 
        motion.Initialize();
    }
}
