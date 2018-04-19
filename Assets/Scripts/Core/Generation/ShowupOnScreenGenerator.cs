using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core;
using ChainedRam.Core.Extentions;
using UnityEngine;

namespace ChainedRam.Core.Generation
{

    public class ShowupOnScreenGenerator : TimedGenerator
    {
        public GameObject Target;

        public PositionProvider PositionProvider; 

        public bool TeleportToOtherSide;
        public bool HideGizmo = true;

        private Vector2? TargetPosition;

        private const float DeltaDistance = 0.01f;

        private float Speed;

        protected override void OnGenerate(GenerateEventArgs e)
        {
            if (TeleportToOtherSide)
            {
                Target.transform.position = PositionProvider.OppositePosition;
            }

            TargetPosition = PositionProvider.ProvidedPosition;

            if (PositionProvider.ProvidedRotation.HasValue)
            {
                Target.transform.eulerAngles = Vector3.forward * PositionProvider.ProvidedRotation.Value;
            }

            Speed = Vector3.Distance(Target.transform.position, TargetPosition.Value) / WaitTime;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

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

            Vector2 sidePos = PositionProvider.ProvidedPosition; 
           
            Gizmos.DrawSphere(sidePos, .25f);

            if (PositionProvider.ProvidedRotation != null)
            {
                Vector2 line = Vector2.up.Rotate(PositionProvider.ProvidedRotation.Value);
                Gizmos.DrawLine(sidePos, sidePos + line);
            }
        }
    }
}