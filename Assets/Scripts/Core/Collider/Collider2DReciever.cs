using ChainedRam.Alebi.Core;
using ChainedRam.Core.Collider;
using ChainedRam.Core.Extentions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Core
{
    /// <summary>
    /// Allows object to recieve a pickable item. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Collider2DReciever<T> : MonoBehaviour where T : Pickable
    {
        /// <summary>
        /// Called when item is recived 
        /// </summary>
        /// <param name="item"></param>
        public abstract void OnReceive(T item); 

        /// <summary>
        /// Find of colided object implements<see cref="IPickable{T}"/> and recieve the damage. 
        /// </summary>
        /// <param name="collision"></param>
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            T pickable;

            if ((pickable = collision.GetComponent(GetTargetPickableType()) as T) != null && pickable.CanBePicked(PickedBy: gameObject))
            {
                pickable.OnPicked(PickedBy: gameObject);
                OnReceive(pickable);
            }
        }

        /// <summary>
        /// Gets Target pickable type used to reciever a component. 
        /// </summary>
        /// <returns></returns>
        protected virtual Type GetTargetPickableType()
        {
            return typeof(T);
        }
    }
}
