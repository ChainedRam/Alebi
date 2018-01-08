using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Selection
{
    public class TriggerSelector : Selector
    {

        private int Count;

        //TODO remove dependency 
        public PoolGenerator Pool;

        public override T Select<T>(T[] list, T prev = null)
        {
            return list[Count];
        }

        public void Next()
        {
            Count++;
            Pool.Selected.End();
        }
    }
}
