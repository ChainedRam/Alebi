using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InstancePooler<T>
{
    private Queue<T> Instances;

    protected const int DefaultCapacity = 50;

    public int Capacity { set; get; }

    public InstancePooler(int capacity = DefaultCapacity)
    { 
        Capacity = capacity; 
        Instances = new Queue<T>(Capacity);
    }

    protected abstract T CreateInstance();

    public T GetInstance()
    {
        T ele; 
        if (Instances.Count < Capacity)
        {
            ele = CreateInstance(); 
            if(ele == null)
            {
                throw new NullReferenceException(); 
            }
        }
        else
        {
           OnReused(Instances.Peek());
           ele = Instances.Dequeue();
        }

        Instances.Enqueue(ele);

        return ele;
    }

    protected virtual void OnReused(T instance)
    {

    }
}

