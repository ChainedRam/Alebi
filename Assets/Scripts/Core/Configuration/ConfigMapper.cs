using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ChainedRam.Core.Configuration
{

    /// <summary>
    /// Defines a configuration method for type T in the scnene. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ConfigMapper<T> : ConfigMapper where T : Component
    {
        ///
        public override Type TargetType { get { return typeof(T); } }

        /// <summary>
        /// Configure object using configuration
        /// </summary>
        /// <param name="target"></param>
        /// <param name="config"></param>
        public override void Configure(object target)
        {
            Configure((T)target);
        }

        /// <summary>
        /// Configure object using configuration
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="config"></param>
        public abstract void Configure(T obj);
    }

    /// <summary>
    /// Configure stuff
    /// </summary>
    public abstract class ConfigMapper : MonoBehaviour
    {
        /// <summary>
        /// Target type to be configured.
        /// </summary>
        public abstract Type TargetType { get; }

        /// <summary>
        /// Configure found objects in scnene. 
        /// </summary>
        /// <param name="target"></param>
        public abstract void Configure(object target);

        /// <summary>
        /// Called once before configuring
        /// </summary>
        public virtual void Prepare() { }
    } 
}