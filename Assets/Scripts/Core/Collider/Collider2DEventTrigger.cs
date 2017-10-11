using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Collider
{
    /// <summary>
    /// Allows object to trigger <see cref="ColliderEventContainer.OnEnter"/> and <see cref="ColliderEventContainer.OnEnter"/> events. 
    /// </summary>
    public class Collider2DEventTrigger : MonoBehaviour
    {
        #region Collider2D events
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            ColliderEventContainer eventContainer;
            if ((eventContainer = collision.GetComponent<ColliderEventContainer>()) != null)
            {
                eventContainer.OnEnter.Invoke();
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            ColliderEventContainer eventContainer;
            if ((eventContainer = collision.GetComponent<ColliderEventContainer>()) != null)
            {
                eventContainer.OnExit.Invoke();
            }
        }
        #endregion
    }
}
