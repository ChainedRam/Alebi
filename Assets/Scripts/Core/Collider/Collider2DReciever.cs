using ChainedRam.Alebi.Core;
using ChainedRam.Alebi.Interface;
using ChainedRam.Core.Collider;
using ChainedRam.Core.Extentions;
using ChainedRam.Core.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Collider2DReciever<T> : MonoBehaviour, IReciever<T>
    {
        /// <summary>
        /// Recieve damage from collision objects that implements <see cref="IPickable{T}"/>
        /// </summary>
        /// <param name="holder"></param>
        public abstract void OnRecieve(T recieved);

        /// <summary>
        /// Find of colided object implements<see cref="IPickable{T}"/> and recieve the damage. 
        /// </summary>
        /// <param name="collision"></param>
        protected void OnTriggerEnter2D(Collider2D collision)
        {
            IPickable<T> holder;

            if (collision.gameObject.HasInterface(typeof(IPickable<T>), out holder))
            {
                holder.OnPicked(this); 
            }
        }
    }
}
