using UnityEngine;
using ChainedRam.Core.Extentions;

namespace ChainedRam.Core.Projection
{
    /// <summary>
    /// A moving unit that contained a rigide body. 
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        [ContextMenuItem("Add Gizmu", "AddGizmu", order = 0)]
        [ContextMenuItem("Remove Gizmu", "RemoveGizmu", order = 1)]
        public Motion Motion;

        private float Delta = 0;

        public bool FaceDirection;

        public virtual void Setup(float delta)
        {
            Delta = delta; 
            enabled = true;
            Motion.Initialize(gameObject, Delta);
        }

        protected virtual void OnEnable()
        {
            Motion.Initialize(gameObject, Delta);
            
            //if motion is not mine 
            if(Motion.gameObject != gameObject)
            {
                //do i have one? 
                Motion mine = GetComponent <Motion> ();
                if(mine != null)
                {
                    Motion = mine; 
                }
                else //copy refrenced one to me 
                {
                    Motion = Motion.CopyTo(gameObject);
                }
            }

        }

        public void FixedUpdate()
        {
            var prevPosition = transform.position; 
            Vector2 offset = Motion.GetOffset();
            transform.position += (Vector3)offset;

            //update angle
            if (FaceDirection)
            {
                gameObject.transform.eulerAngles = Vector3.forward * (PositionProvider.AngleBetween(prevPosition, transform.position));
            }
        }

        #region ContextMenuItem
        private void AddGizmu()
        {
            ProjectileGizmo gizmo = GetComponent<ProjectileGizmo>() ?? gameObject.AddComponent<ProjectileGizmo>();

            gizmo.projectile = this;
        }

        private void RemoveGizmu()
        {
            ProjectileGizmo gizmo = GetComponent<ProjectileGizmo>();

            if (gizmo != null)
            {
                DestroyImmediate(gizmo);
            }
            else
            {
                Debug.LogWarning("Gizmo doesn't exist on this game object.");
            }
        } 
        #endregion
    }
}
