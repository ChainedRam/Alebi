using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Interface
{
    /// <summary>
    /// Holds an item to be delivered by <see cref="IReciever{T}."/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPickable<T>
    {
        /// <summary>
        /// Source object that is holding it
        /// </summary>
        object Source { get; }

        /// <summary>
        /// Called when Item is picked  
        /// </summary>
        /// <param name="reciever"></param>
        void OnPicked(IReciever<T> reciever);
    }
}
