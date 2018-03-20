using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core;
using UnityEngine;

namespace ChainedRam.Core.Generation
{

    public class ShowupOnScreenGenerator : TimedGenerator
    {
        public GameObject Target;
        public Direction Side;
        public PositionRelativeTo OffsetRelative;
        
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
                Target.transform.position = PositionProvider.GetScreenPosition(Side, Offset, (PositionRelativeTo)(-1*(int)OffsetRelative)); 
            }

            TargetPosition = PositionProvider.GetScreenPosition(Side, Offset, OffsetRelative); 

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

            Vector2 sidePos = PositionProvider.GetScreenPosition(Side, Offset, OffsetRelative);
           
            Gizmos.DrawSphere(sidePos, .5f);
        }


    }
}