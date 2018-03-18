using ChainedRam.Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis
{
    Horizontal, 
    Vertical
}

public class Tracking : MonoBehaviour
{
    public GameObject Target;

    public Axis TrackingAxis;

    void Update()
    {
        if (TrackingAxis == Axis.Horizontal)
        {
            transform.position = new Vector3(transform.position.x, Target.transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(Target.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
