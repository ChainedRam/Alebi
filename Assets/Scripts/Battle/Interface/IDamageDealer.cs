using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Interface
{
    /// <summary>
    /// Attach to which that can deal damage 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDamageDealer<T>
    {
        IDamage<T> Deal(IDamageReciever<T> reciever, IDamage<T> dmg);
    }
}
