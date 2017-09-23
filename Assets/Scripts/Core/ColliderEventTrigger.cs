using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Core
{
    /// <summary>
    /// Allows object to trigger <see cref="ColliderEventContainer"/> Enter and exit events. 
    /// </summary>
    public class ColliderEventTrigger : MonoBehaviour
    {
        #region Collider2D events
        private void OnTriggerEnter2D(Collider2D collision)
        {
            ColliderEventContainer eventContainer;
            if ((eventContainer = collision.GetComponent<ColliderEventContainer>()) != null)
            {
                eventContainer.OnEnter.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
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
