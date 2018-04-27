using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjMotion =  ChainedRam.Core.Projection.Motion; 

public class MotionGizmo : MonoBehaviour
{
    public Transform StartingPoint;

    public ProjMotion Motion; 

    [Range(10, 100)]
    public float MaxLineLength = 10;

    [Range(200, 2000)]
    public int MaxLineDrawn = 1000;

    private void Start()
    {
        StartingPoint = null;
    }

    private void OnDrawGizmos()
    {
        if (StartingPoint == null)
        {
            return;
        }

        Motion.Initialize(StartingPoint.gameObject);

        Color[] c = { Color.white, Color.green, Color.blue, Color.red, Color.green };
        Color prev = Gizmos.color;

        Vector3 to, from = StartingPoint.transform.position;
        int i;
        float lineLength;

        for (i = 0, lineLength = 0; lineLength <= MaxLineLength && i < MaxLineDrawn; i++, lineLength += Vector2.Distance(from, to), from = to)
        {
            Gizmos.color = c[i % c.Length];
            to = from + (Vector3)Motion.GetOffset();
            Gizmos.DrawLine(from, to);
        }
        Gizmos.color = prev;
        Motion.Initialize(StartingPoint.gameObject);
    }

    private void OnValidate()
    {
        ProjMotion p = GetComponent<ProjMotion>();
        if (Motion == null && p != null)
        {
            Motion = p; 
        }

        if(StartingPoint == null)
        {
            StartingPoint = this.transform; 
        }
    }
}
