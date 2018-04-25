using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilBreaker : MonoBehaviour
{
    public bool DisableOnHit; 

    public void OnTriggerEnter2D(Collider2D collision)
    {
        OilBreakCollision oil = collision.GetComponent<OilBreakCollision>(); 
        if(oil != null)
        {
            oil.OnHit(this);

            if (DisableOnHit)
            {
                gameObject.SetActive(false);
            }
        }

        
    }

}
