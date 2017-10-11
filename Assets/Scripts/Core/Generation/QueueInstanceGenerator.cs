using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QueueInstanceGenerator<T> : InstanceGenerator<T> where T : Component
{
    [Header("FixedSizeGenerator Settings")]
    public int MaxSize;

    private Queue<T> GenerationQueue;

    public override ICollection<T> Instances
    {
        get
        {
            return (ICollection<T>) GenerationQueue; 
        }
    }
    private void Awake()
    {
        GenerationQueue = new Queue<T>();
        OnGenerate += GenerationQueue.Enqueue;
    }

    protected override T GetInstance()
    {
        T comp;

        if (GenerationQueue.Count < MaxSize)
        {
            comp = base.GetInstance();
        }
        else
        {
            comp = GenerationQueue.Dequeue(); 
        }
        return comp; 
    }
}
