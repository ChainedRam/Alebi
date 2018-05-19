using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFadeOut : MonoBehaviour
{
    public float Duration;

    private SpriteRenderer Sprite;
    private float? Speed;
    private float StartingAlpha;

    private void Awake()
    {
        Sprite = GetComponent<SpriteRenderer>();
        StartingAlpha = Sprite.color.a;
    }

    public void StartFading()
    {
        enabled = true; 
    }

    private void OnEnable()
    {
        Sprite.color = new Color(Sprite.color.r, Sprite.color.g, Sprite.color.b, StartingAlpha);
    }

    void FixedUpdate ()
    {
        if(Speed == null)
        {
            Speed = Sprite.color.a / (Duration / Time.fixedDeltaTime);
        }

        Sprite.color = new Color(Sprite.color.r, Sprite.color.g, Sprite.color.b, Sprite.color.a - Speed.Value); 
    }
}
