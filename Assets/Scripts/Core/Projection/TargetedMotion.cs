using ChainedRam.Core.Extentions;
using ChainedRam.Core.Projection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Projection
{
    public class TargetedMotion : AbsoluteMotion
    {
        public PositionProvider Target;

        [Range(1f, 359)]
        public float Degree = 1;

        public float Speed = 0.1f;

        public Motion AlternativeMotion; 

        public bool Invert;
        public bool HideGizmo = true;

        private GameObject Sender; 

        private const float StepSize = 0.005f;
        private float DegreeToTarget;
        private float DegreeStepLength;
        private float DegreeStepSize;
        private float StepCounter;
        private float Delta;
        private bool HasInitAlernative; 

        public override void Initialize(GameObject sender, float delta = 1)
        {
            base.Initialize(sender, delta);
            Sender = sender;
            Delta = delta;
            HasInitAlernative = false; 
            FancyMath();
        }

        public override Vector2 GetRelativeOffset(Vector2 defaultVector)
        {
            if (StepCounter > DegreeStepLength)
            {
                if(AlternativeMotion != null)
                {
                    if (HasInitAlernative == false)
                    {
                        AlternativeMotion.Initialize(Sender, Delta);
                        HasInitAlernative = true; 
                    }

                    return AlternativeMotion.GetRelativeOffset(defaultVector); 
                }

                return defaultVector;
            }

            float speedBuild = 0;

            while (speedBuild < Speed)
            {
                    defaultVector += (Vector2.left * StepSize).Rotate(DegreeToTarget + (Invert ? 180 : 0) + (StepCounter++ * DegreeStepSize * (Invert? -1 : 1)) + (-Degree / 2));

                if (StepCounter + 1 > DegreeStepLength)
                {
                    return defaultVector + (Vector2.left * StepSize).Rotate(DegreeToTarget+ (Invert ? 180 : 0) + (DegreeStepLength * DegreeStepSize* (Invert ? -1 : 1)) + (-Degree / 2));
                }

                speedBuild += StepSize;
            }

            return defaultVector;
        }


        public override Motion CopyTo(GameObject go)
        {
            TargetedMotion copy = go.AddComponent<TargetedMotion>();

            copy.Target = Target.Copy();
            copy.Degree = Degree;
            copy.Speed = Speed;
            copy.Invert = Invert;
            copy.HideGizmo = HideGizmo;
            copy.Sender = Sender;
            copy.DegreeToTarget = DegreeToTarget;
            copy.DegreeStepLength = DegreeStepLength;
            copy.DegreeStepSize = DegreeStepSize;
            copy.StepCounter= StepCounter;
            copy.AlternativeMotion = AlternativeMotion == null? null : AlternativeMotion.CopyTo(go); 
                    
            return copy;
        }

        private void FancyMath()
        {
            var lineCenter = (Sender.transform.position + Target.ProvidedPosition) / 2;
            float L = Vector3.Distance(Sender.transform.position, Target.ProvidedPosition);
            float X = (L) / (Mathf.Tan((Degree / 2) * Mathf.Deg2Rad) * 2);
            float Xdiff = Sender.transform.position.x - Target.ProvidedPosition.x;
            float Ydiff = Sender.transform.position.y - Target.ProvidedPosition.y;

            DegreeToTarget = Mathf.Atan2(Ydiff, Xdiff) * Mathf.Rad2Deg;

            var offset = (Vector2.right * X).Rotate(-90 + DegreeToTarget);
            var circleCenter = lineCenter + (Vector3)offset;
            float radius = Vector3.Distance(circleCenter, Sender.transform.position);
            float arcLength = radius * Degree * Mathf.Deg2Rad;

            DegreeStepLength = arcLength / StepSize;

            DegreeStepSize = Degree / DegreeStepLength;       
            StepCounter = 0;
        }

        private void OnDrawGizmos()
        {
            if (HideGizmo || Sender == null)
            {
                return;
            }
            float TotalDegree = Degree;

            //line between
            Gizmos.DrawLine(Sender.transform.position, Target.ProvidedPosition);

            //center point 
            var lineCenter = (Sender.transform.position + Target.ProvidedPosition) / 2;
            Gizmos.DrawSphere(lineCenter, 0.1f);


            //Cicrle center 
            float L = Vector3.Distance(Sender.transform.position, Target.ProvidedPosition);
            float X = (L) / (Mathf.Tan((TotalDegree / 2) * Mathf.Deg2Rad) * 2);

            float Xdiff = Sender.transform.position.x - Target.ProvidedPosition.x;
            float Ydiff = Sender.transform.position.y - Target.ProvidedPosition.y;

            float degree = Mathf.Atan2(Ydiff, Xdiff) * Mathf.Rad2Deg;

            var offset = (Vector2.right * X).Rotate(-90 + degree);

            //[important]
            var circleCenter = lineCenter + (Vector3)offset;
            Gizmos.DrawSphere(circleCenter, 0.1f);


            float radius = Vector3.Distance(circleCenter, Sender.transform.position);

            //length 
            float arcLength = radius * TotalDegree * Mathf.Deg2Rad;

            float degreeStepCount = (int)(Mathf.Ceil((float)(arcLength / StepSize)));

            float degreeStep = TotalDegree / degreeStepCount *(Invert ? -1 : 1);

            Vector2 from = Sender.transform.position;
            Vector2 to;
            for (int i = 0; i < degreeStepCount && i < 1000000; i++)
            {
                to = from + (Vector2.up * (float)StepSize).Rotate((Invert ? 180 : 90) + (degree) + (i * degreeStep) + (-Degree / 2));
                Gizmos.DrawLine(from, to);
                from = to;
            }
        }
    }

}