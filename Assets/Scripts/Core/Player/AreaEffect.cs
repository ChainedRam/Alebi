using UnityEngine;

namespace ChainedRam.Core.Player
{
    /// <summary>
    /// Used to implement a trigger collider effect with <see cref="Player"/>
    /// </summary>
    public abstract class AreaEffect : MonoBehaviour
    {
        /// <summary>
        /// Adds effect to player. Called in <see cref="Player.OnTriggerEnter2D(Collider2D)"/>
        /// </summary>
        /// <param name="player"></param>
        public abstract void AddEffect(Player player);
        
        /// <summary>
        /// Refreshes effect on player. Called in <see cref="Player.OnTriggerStay2D(Collider2D)"/>
        /// </summary>
        /// <param name="player"></param>
        public abstract void RefreshEffect(Player player);

        /// <summary>
        /// Removes effect from player. Called in <see cref="Player.OnTriggerExit2D(Collider2D)"/>
        /// </summary>
        /// <param name="player"></param>
        public abstract void RemoveEffect(Player player);
    }
}
