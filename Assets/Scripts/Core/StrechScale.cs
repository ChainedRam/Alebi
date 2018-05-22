using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrechScale : MonoBehaviour
{
    private float Speed;
    private Vector3 StartingScale; 

    public Vector3 StrechTo;

    public float Duration; 

    private void Awake()
    {
        Speed =  Vector3.Distance(transform.localScale, StrechTo) / (Duration);
        StartingScale = transform.localScale; 
        enabled = true;
    }

    private void OnEnable()
    {
        transform.localScale = StartingScale; 
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
