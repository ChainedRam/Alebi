using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemShatterer : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShatterCollision g = collision.gameObject.GetComponent<ShatterCollision>(); 

        if(g != null)
        {
            g.OnHit(this); 
        }
    }
}
