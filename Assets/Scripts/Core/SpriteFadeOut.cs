using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFadeOut : MonoBehaviour
{
    public float Duration;

    private SpriteRenderer Sprite;
    private float? Speed; 

    

    public void Start ()
    {
        Sprite = GetComponent<SpriteRenderer>(); 
    }
     
    public void StartFading()
    {
        enabled = true; 
    }


    void FixedUpdate ()
    {
        if(Speed == null)
        {
            Speed = Sprite.color.a / (Duration / Time.fixedDeltaTime);
        }

        //print(Sprite.color.a);
        
        Sprite.color = new Color(Sprite.color.r, Sprite.color.g, Sprite.color.b, Sprite.color.a - Speed.Value); 
    }
}
