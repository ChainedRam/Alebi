using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Extentions
{
    public static class GameObjectExtention
    {
        //TODO memorize results. 
        /// <summary>
        /// Checks if any of game objects components implement given interface. 
        /// </summary>
        /// <param name="go"></param>
        /// <param name="t"></param>
        /// <returns>The first component that implements that interface</returns>
        public static bool HasInterface<T>(this GameObject go, Type type, out T instance)
        {
            if (!type.IsInterface)
            {
                throw new Exception(type.Name + " is not an interface.");
            }

            Component[] comps = go.GetComponents<MonoBehaviour>();

            foreach (Component comp in comps)
            {
                //can get deleted midway 
                if (comp == null)
                {
                    continue;
                }

                Type compType = comp.GetType();

                Type[] interfaces = compType.GetInterfaces();

                foreach (var inter in interfaces)
                {
                    if (inter == type)
                    {
                        instance = (T)(object)comp; //rip boxing 
                        return true;
                    }
                }
            }


            instance = default(T);

            return false;
        }
    }
}
