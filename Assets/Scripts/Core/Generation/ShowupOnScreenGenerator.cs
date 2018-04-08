using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core;
using UnityEngine;

namespace ChainedRam.Core.Generation
{

    public class ShowupOnScreenGenerator : TimedGenerator
    {
        public GameObject Target;

        public PositionProvider PositionProvider; 

        public int TargetRotation;


        public bool TeleportToOtherSide;
        public bool HideGizmo;

        private Vector2? TargetPosition;

        private const float DeltaDistance = 0.01f;

        public float Speed;

        protected override void OnGenerate(GenerateEventArgs e)
        {
            if (TeleportToOtherSide)
            {
                Target.transform.position = PositionProvider.OppositePosition;
            }

            TargetPosition = PositionProvider.ProvidedPosition;

            Target.transform.eulerAngles = Vector3.forward * TargetRotation;
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
           
            Gizmos.DrawSphere(sidePos, .5f);
        }


    }
}