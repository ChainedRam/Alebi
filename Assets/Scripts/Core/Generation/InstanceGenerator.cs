using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InstanceGenerator<T> : Generator where T : Component
{
    [Header("Instance Generator")]
    public T Prefab;

    public Transform GenerateAt;

    public abstract ICollection<T> Instances { get; }

    /// <summary>
    /// Called after
    /// </summary>
    public event Action<T> OnGenerate;

    public override void Generate()
    {
        T instance = GetInstance();
        Instances.Add(instance);
        SetupGenerated(instance);

        OnGenerate(instance); 
    }

    public virtual void DegenerateAll()
    {
        foreach(T i in Instances)
        {
            Degenerate(i);
        }
    }

    public virtual void Degenerate(T instance)
    {
        Instances.Remove(instance);
        Destroy(instance.gameObject);
    }

    protected virtual T GetInstance()
    {
        T instance = Instantiate(Prefab, GenerateAt.transform);

        instance.gameObject.SetActive(true);
        instance.transform.localPosition = Vector2.zero;
        instance.transform.localRotation = Quaternion.identity;

        return instance;
    }
   
    public abstract void SetupGenerated(T generated); 
}
