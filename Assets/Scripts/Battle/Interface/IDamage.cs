using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Interface
{
    /// <summary>
    /// Damage Unit 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDamage<T>
    {
        T Damage { get; }
    }
}
