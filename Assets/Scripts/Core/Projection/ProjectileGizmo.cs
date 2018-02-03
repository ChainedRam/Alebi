using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGizmo : MonoBehaviour
{
    public Projectile projectile;

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
        Motion motion = projectile.motion;
        motion.Initialize();
        int size = 10;
        Color[] c = { Color.white, Color.green, Color.blue, Color.red, Color.green };
        Color prev = Gizmos.color; 
        Vector3 from = projectile.transform.position;

        int i = 0;
        float sum = 0;
        while ( sum <= size)
        {
            Gizmos.color = c[i % c.Length];
            Vector3 to = from + (Vector3)motion.GetOffset();
            Gizmos.DrawLine(from, to);

            i++;
            float line = Vector2.Distance(from, to);
            from = to;
            sum += line;
        }
        Gizmos.color = prev; 
        motion.Initialize();
    }
}
