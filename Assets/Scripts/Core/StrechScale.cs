using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrechScale : MonoBehaviour
{
    private float Speed;

    public Vector3 StrechTo;

    public float Duration; 

    private void Awake()
    {
        enabled = true;
        Speed =  Vector3.Distance(transform.localScale, StrechTo) / (Duration); 
    }

    public void BeginStreching()
    {
        enabled = true; 
    }

    private void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, StrechTo, Speed * Time.deltaTime); 
    }
}
