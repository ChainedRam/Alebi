using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Interface
{
    /// <summary>
    /// A reciever of a held item <see cref="IReciever{T}."/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReciever<T>
    {
        /// <summary>
        /// Recieves an item. 
        /// </summary>
        /// <param name="holder"></param>
        void Recieve(IHolder<T> holder);
    }
}
