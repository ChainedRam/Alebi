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

        int size = 100;

        Vector3 from = projectile.transform.position;

        for (int i = 0; i < size; i++)
        {
            Vector3 to = from + (Vector3)motion.GetOffset();
            Gizmos.DrawLine(from, to);

            from = to;
        }

        motion.Initialize();

    }
}
