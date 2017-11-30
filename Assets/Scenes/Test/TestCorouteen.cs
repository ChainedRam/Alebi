using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCorouteen : MonoBehaviour // Class > Inheretence > Overriding  
{
    public bool IsWalking;

    public int AnimationFrame;

    public float WaitTime;

    public Rigidbody2D body; 


    public bool IsAnimating; 
    // Use this for initialization
    void Start ()
    {
        
        StartCoroutine(CoroutineMethodName()); 
    }

    IEnumerator CoroutineMethodName()
    {
        while (true)
        {
            //SomeLogic
            yield return new WaitForSeconds(1);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        Vector2 position1 = Vector2.one;
        Vector2 position2 = Vector2.one;

        float maxTravelDistence = 0; 

        gameObject.transform.position = Vector2.MoveTowards(position1, position2, maxTravelDistence); //Vector2, Vector2, float

        float DistanceToMove = 0;
        Vector2 TargetPosition; 

        TargetPosition = gameObject.transform.position + (Vector3.up * DistanceToMove); 

        //phsysics logic 
        if (IsWalking)
        {
            Debug.Log("I'm walking");
        }
        else
        {
            return;
        }
    }

   
}
