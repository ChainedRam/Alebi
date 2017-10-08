using ChainedRam.Alebi.Core;
using ChainedRam.Alebi.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Core
{
    public abstract class DamageReciever<T> : ColliderEventTrigger, IDamageReciever<T>
    {
        /// <summary>
        /// Recieve damage from collision objects that implements <see cref="IDamage{T}"/>
        /// </summary>
        /// <param name="dmg"></param>
        public abstract void RecieveDamage(IDamage<T> dmg);

        /// <summary>
        /// It works, that all that matters. 
        /// 
        /// Find of colided object implements<see cref="IDamage{T}"/> and recieve the damage. 
        /// </summary>
        /// <param name="collision"></param>
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            Type DamageType = typeof(IDamage<T>);

            Component[] comps = collision.gameObject.GetComponents<MonoBehaviour>();

            foreach (Component comp in comps)
            {
                if (comp == null)
                {
                    continue;
                }

                Type compType = comp.GetType();

                Type[] interfaces = compType.GetInterfaces();

                foreach (var inter in interfaces)
                {
                    if ((inter == (DamageType)))
                    {
                        RecieveDamage((IDamage<T>)comp);
                        return;
                    }
                }
            }
        }
    }
}
