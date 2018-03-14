using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Generation;
using UnityEngine;

public enum ShowupMovement
{
    ToInside = 0, 
    ToOutSide = 1
}

public class ShowupOnScreen : TimedGenerator
{
    
    public GameObject Target;
    public Direction Side;
    public ShowupMovement Movement;

    public int TargetRotation; 
    public float Offset;

    public bool TeleportToOtherSide; 
    public bool HideGizmo;

    private Vector2? TargetPosition;

    private const float DeltaDistance = 0.01f;

    public float Speed; 

    protected override void OnGenerate(GenerateEventArgs e)
    {
        if(TeleportToOtherSide)
        {
            Target.transform.position = GetSidePosition(Side, Offset, (ShowupMovement)((((int)Movement)+1)%2));
        }


        TargetPosition = GetSidePosition(Side, Offset, Movement);

        Target.transform.eulerAngles = Vector3.forward * TargetRotation; 
        Speed = Vector3.Distance(Target.transform.position, TargetPosition.Value) / WaitTime;
    }

    protected override void Update()
    {
        base.Update();

        if(TargetPosition != null)
        {
            
            Target.transform.position = Vector3.MoveTowards(Target.transform.position, TargetPosition.Value, Speed*Time.deltaTime);


            float distrance = Vector3.Distance(Target.transform.position, TargetPosition.Value);
            if (distrance < DeltaDistance)
            {
                TargetPosition = null; 
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(HideGizmo)
        {
            return; 
        }

        Vector2 sidePos = GetSidePosition(Side, Offset, Movement);
        //Vector2 targetposition = GetSidePosition(Side, Offset, (ShowupMovement)(((int)Movement+1)%2));

        Gizmos.DrawSphere(sidePos, .5f);
    }

    private float GetScreenWidth()
    {
        return GetScreenHeight() * Camera.main.aspect; 
    }
    private float GetScreenHeight()
    {
        return 2 * Camera.main.orthographicSize;
    }

    public Vector2 GetSidePosition(Direction d, float offset, ShowupMovement m)
    {
        int side = (int)d;
        int sideSign = (2 * (side % 2) - 1);
        int offsetSign = ((int)m * 2 - 1); 
        int isVertical =  1-(side / 2);
        int isHorizontal = (side / 2);

        float width = isHorizontal * (sideSign * GetScreenWidth()/2 + offset * sideSign * offsetSign);
        float height = isVertical * (sideSign * GetScreenHeight()/2 + offset * sideSign * offsetSign); 

        return new Vector2(width,height);
    }
}

