using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An InstanceGenerator that holds limited generations in a queue to recycle them. 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class QueueInstanceGenerator<T> : InstanceGenerator<T> where T : Component
{
    #region Inspector Attributes 
    [Header("FixedSizeGenerator Settings")]
    public int MaxSize = 50; //default size 
    #endregion
    #region Private Attributes 
    /// <summary>
    /// Holds generated instances
    /// </summary>
    private Queue<T> GenerationQueue;
    #endregion
    #region Override InstanceGenerator Property  
    protected override IEnumerable<T> Instances
    {
        get
        {
            return GenerationQueue; 
        }
    }
    #endregion
    #region Unity Method
    private void Awake()
    {
        GenerationQueue = new Queue<T>();
    }
    #endregion
    #region Override InstanceGenerator Methods  
    protected override T CreateInstance()
    {
        T comp;

        if (GenerationQueue.Count < MaxSize)
        {
            comp = base.CreateInstance();
        }
        else
        {
            comp = GenerationQueue.Dequeue(); 
        }
        return comp; 
    }

    public override void AddInstance(T instance)
    {
        GenerationQueue.Enqueue(instance); 
    }

    public override void RemoveInstance(T instance)
    {
        GenerationQueue.Dequeue(); 
    }
    #endregion
}
