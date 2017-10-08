using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Interface
{
    /// <summary>
    /// An object that can deal damage 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDamageReciever<T>
    {
        void RecieveDamage(IDamage<T> dmg);
    }
}
