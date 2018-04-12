using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Extentions
{
    public static class GameObjectExtention
    {
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

        /// <summary>
        /// Copys component to a destination game object 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        [Obsolete("Doesn't copy pointers, rather shares them.")]
        public static T CopyComponent<T>(this GameObject destination, T original) where T : Component
        {
            Type type = original.GetType();
            Component copy = destination.AddComponent(type);
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                field.SetValue(copy, field.GetValue(original));
            }
            return copy as T;
        }

        /// <summary>
        /// Copys component to a destination game object 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        [Obsolete("Doesn't copy pointers, rather shares them.")]
        public static Component CopyComponent(this GameObject destination, Component original)
        {
            System.Type type = original.GetType();
            Component copy = destination.AddComponent(type);
            // Copied fields can be restricted with BindingFlags
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                field.SetValue(copy, field.GetValue(original));
            }
            return copy;
        }
    }
}
