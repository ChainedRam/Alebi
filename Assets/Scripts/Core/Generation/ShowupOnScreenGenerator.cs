using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Generation;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    public enum Direction
    {
        None = 0x00,
        //Single
        North = 0x01, 
        South = 0x02, 
        East = 0x04, 
        West = 0x08, 
        //Combo
        NorthEast = North | East,
        NorthWest = North | West, 
        SouthEast = South | East, 
        SouthWest = South | West, 
    }

    public enum ShowupMovement
    {
        ToInside = 0,
        ToOutSide = 1, 
    }

    public class ShowupOnScreenGenerator : TimedGenerator
    {

        public GameObject Target;
        public Direction Side;
        public ShowupMovement Movement;

        public int TargetRotation;
        public Vector2 Offset;

        public bool TeleportToOtherSide;
        public bool HideGizmo;

        private Vector2? TargetPosition;

        private const float DeltaDistance = 0.01f;

        public float Speed;

        protected override void OnGenerate(GenerateEventArgs e)
        {
            if (TeleportToOtherSide)
            {
                Target.transform.position = GetSidePosition(Side, Offset, (ShowupMovement)((((int)Movement) + 1) % 2));
            }

            TargetPosition = GetSidePosition(Side, Offset, Movement);

            Target.transform.eulerAngles = Vector3.forward * TargetRotation;
            Speed = Vector3.Distance(Target.transform.position, TargetPosition.Value) / WaitTime;
        }

        protected override void Update()
        {
            base.Update();

            if (TargetPosition != null)
            {

                Target.transform.position = Vector3.MoveTowards(Target.transform.position, TargetPosition.Value, Speed * Time.deltaTime);


                float distrance = Vector3.Distance(Target.transform.position, TargetPosition.Value);
                if (distrance < DeltaDistance)
                {
                    TargetPosition = null;
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (HideGizmo)
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

        public Vector2 GetSidePosition(Direction d, Vector2 offset, ShowupMovement m)
        {
            int mov = m == ShowupMovement.ToInside ? 1 : -1;

            float width = offset.x;
            float height = offset.y;

            if(d.HasFlag(Direction.North))
            {
                height = GetScreenHeight() / 2 - (mov * offset.y); 
            }
            else if(d.HasFlag(Direction.South))
            {
                height = -GetScreenHeight() / 2 + (mov * offset.y);
            }

            if (d.HasFlag(Direction.East))
            {
                width = GetScreenWidth() / 2 - (mov * offset.x);
            }
            else if (d.HasFlag(Direction.West))
            {
                width = -GetScreenWidth() / 2 + (mov * offset.x);
            }

            return new Vector2(width, height);
        }
    }
}