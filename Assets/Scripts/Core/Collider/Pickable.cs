using UnityEngine;

namespace ChainedRam.Core.Collider
{
    /// <summary>
    /// Objects with this script that can be picked (recieved) by <see cref="Collider2DReciever{T}"/>
    /// </summary>
    //TODO think about a generic alternative 
    public abstract class Pickable : MonoBehaviour
    {
        /// <summary>
        /// Determins if item can be picked. 
        /// </summary>
        /// <param name="PickedBy"></param>
        /// <returns></returns>
        public abstract bool CanBePicked(GameObject PickedBy);

        /// <summary>
        /// Gets called if item is picked
        /// </summary>
        /// <param name="PickedBy"></param>
        public abstract void OnPicked(GameObject PickedBy);
    }
}