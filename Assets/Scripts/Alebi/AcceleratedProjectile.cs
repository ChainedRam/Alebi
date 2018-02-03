using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratedProjectile : Rigidbody2DProjectile
{
    public Vector2 Acceleration;

    private void FixedUpdate()
    {
        body.velocity += Acceleration * Time.deltaTime;
    }
}
