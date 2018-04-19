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

        public virtual void Setup(float delta)
        {
            Delta = delta; 
            enabled = true;
        }

        protected virtual void OnEnable()
        {
            Motion.Initialize(gameObject, Delta);
        }

        public void FixedUpdate()
        {
            Vector2 offset = Motion.GetOffset();
            transform.position += (Vector3)offset; 
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
