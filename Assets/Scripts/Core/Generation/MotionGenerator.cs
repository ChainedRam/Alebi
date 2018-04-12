using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Extentions;
using ChainedRam.Core.Projection;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    public class MotionGenerator : TimedGenerator
    {
        public GameObject Target;
        public Projection.Motion Motion;

        public bool FaceDirection; 

        private Projection.Motion CopiedMotion;


        protected override bool ShouldGenerate()
        {
            return true; 
        }

        protected override void OnGenerate(GenerateEventArgs e)
        {
            var offset = CopiedMotion.GetOffset();

            //update position 
            Vector2 prevPosition = Target.transform.position; 
            Target.transform.position += (Vector3)offset; 

            //update angle
            if (FaceDirection)
            {
                Target.gameObject.transform.eulerAngles = Vector3.forward * (PositionProvider.AngleBetween(prevPosition, Target.transform.position));
            }
        }

        protected override void OnBegin()
        {
            base.OnBegin();
            CopiedMotion = Motion.CopyTo(gameObject); 

            CopiedMotion.Initialize(Target, Delta);
        }

        protected override void OnEnd()
        {
            base.OnEnd();

            if (CopiedMotion != null)
            {
                Destroy(CopiedMotion);
            }
        }
    }
}