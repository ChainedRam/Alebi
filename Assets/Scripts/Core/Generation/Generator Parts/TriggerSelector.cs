using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSelector : Selector {

    private int Count;

    public PoolGenerator Pool;

    public override T Select<T>(T[] list, T prev = null)
    {
        return list[Count];
    }

    public void Next()
    {
        Count++;
        Pool.Selected.EndGenerating();
    }

}
