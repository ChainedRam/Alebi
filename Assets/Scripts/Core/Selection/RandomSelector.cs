using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Selection
{
    public class RandomSelector : Selector
    {
        public bool NoRepeat; 

        public override T Select<T>(T[] list, T prev = null)
        {
            if(list.Length  == 0)
            {
                throw new Exception("Cannot select from empty list"); //TODO custom exception
            }

            if (list.Length == 1 && NoRepeat)
            {
                Debug.LogError("List contains 1 item and is set to NoRepeat");
            }

            T t;
            do
            {
               t = list[UnityEngine.Random.Range(0, list.Length)];
            } while (NoRepeat && t == prev);

            return t; 
        }
    }
}
