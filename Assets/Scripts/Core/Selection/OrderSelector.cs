﻿using ChainedRam.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Selection
{
    public class OrderSelector : Selector
    {
        public bool Loop;

        private int Index;

        private void Start()
        {
            Index = 0;
        }

        public override T Select<T>(T[] list, T prev = null)
        {
            if (list.Length == 0)
            {
                throw new Exception("Cannot select from empty list"); //TODO custom exception
            }

            return list[Index++ % (Loop ? list.Length : int.MaxValue)];
        }

        public void ResetIndex()
        {
            Index = 0;
        }

        public void Skip()
        {
            Skip(1);
        }

        public void Skip(int skips)
        {
            Index += skips;
        }


    } 
}
