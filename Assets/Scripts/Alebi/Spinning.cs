using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{

    public float AngularSpeed;

    private void LateUpdate()
    {
        transform.eulerAngles += Vector3.forward * AngularSpeed;
    }
}
