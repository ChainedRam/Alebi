using UnityEngine;
using ChainedRam.Core.Extentions;

namespace ChainedRam.Core.Projection
{
    /// <summary>
    /// A moving unit that contained a rigide body. 
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        [ContextMenuItem("Add Gizmu", "AddGizmu", order =0)]
        [ContextMenuItem("Remove Gizmu", "RemoveGizmu", order =1)]
        public Motion Motion;

        private void Start()
        {
            Motion.Initialize(); 
        }

        public virtual void Setup(float delta)
        {
            Motion.Initialize(delta); 
        }

        public void FixedUpdate()
        {
            Vector2 offset = Motion.GetOffset(); 

            transform.localPosition += (Vector3)offset.Rotate(transform.rotation.eulerAngles.z);
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
