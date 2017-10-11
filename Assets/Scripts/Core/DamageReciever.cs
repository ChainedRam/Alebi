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
    public abstract class Trigger2DReciever<T> : Collider2DEventTrigger, IReciever<T>
    {
        /// <summary>
        /// Recieve damage from collision objects that implements <see cref="IHolder{T}"/>
        /// </summary>
        /// <param name="holder"></param>
        public abstract void Recieve(IHolder<T> holder);

        /// <summary>
        /// It works, that all that matters. 
        /// 
        /// Find of colided object implements<see cref="IHolder{T}"/> and recieve the damage. 
        /// </summary>
        /// <param name="collision"></param>
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            IHolder<T> holder;

            if (collision.gameObject.HasInterface(typeof(IHolder<T>), out holder))
            {
                Recieve(holder); 
            }
        }
    }
}
