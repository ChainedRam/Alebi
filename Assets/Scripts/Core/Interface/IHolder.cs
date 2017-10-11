using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Interface
{
    /// <summary>
    /// Holds an item to be delivered by <see cref="IReciever{T}."/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHolder<T>
    {
        /// <summary>
        /// Item held.
        /// </summary>
        T Item { get; }
    }
}
