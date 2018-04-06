using ChainedRam.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Selection
{
    public class OrderSelector : Selector
    {
        public int Repeat = 0;

        private int Index;

        public override T Select<T>(T[] list, T prev = null)
        {
            if (list.Length == 0)
            {
                throw new Exception("Cannot select from empty list"); //TODO custom exception
            }

            if (Repeat > 1 && Index + 1 == list.Length)
            {
                Repeat--;
                Index = 0;
            }

            if (Index < list.Length)
            {
                return list[(Index++)];
            }
            else
            {
                return null;
            }
        }

        public override void ResetSelector()
        {
            Index = 0;
        }
    } 
}
