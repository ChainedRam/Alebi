using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

/// <summary>
/// A generator that generates components. 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class InstanceGenerator<T> : Generator where T : Component
{
    #region Inspector Attribute 
    [Header("Instance Generator")]
    [Tooltip("Prefab to genrate.")]
    public T Prefab;

    [Tooltip("Parnet to generate in.")]
    public Transform GenerateAt;
    #endregion
    #region Protected Abstract Property
    /// <summary>
    /// A collection holding generated instance.
    /// </summary>
    protected virtual IEnumerable<T> Instances
    {
        get
        {
            return collection;
        }
    }
    #endregion
    #region Private Attributes
    private Collection<T> collection;
    #endregion
    #region Public Events
    /// <summary>
    /// Called after generating an instance. 
    /// </summary>
    public event Action<T> OnInstanceGenerated;
    #endregion
    #region Public Methods
    protected virtual void DegenerateAll()
    {
        foreach(T i in Instances)
        {
            Degenerate(i);
        }
    }

    protected virtual void Degenerate(T instance)
    {
        RemoveInstance(instance);
        Destroy(instance.gameObject);
    }

    protected virtual T CreateInstance()
    {
        return Instantiate(Prefab, GenerateAt);
    }
   
    public virtual void SetupGenerated(T instance)
    {
        instance.gameObject.SetActive(true);
        instance.transform.localPosition = Vector2.zero;
        instance.transform.rotation = Quaternion.identity; 
    }
    #endregion
    #region Override Generator
    protected override void WhenAwake()
    {
        base.WhenAwake();
        collection = new Collection<T>();
        OnGenerate += GenerateInstance;
    }
    #endregion
    #region Private Methods
    /// <summary>
    /// 
    /// </summary>
    private void GenerateInstance()
    {
        T instance = CreateInstance();
        AddInstance(instance);
        SetupGenerated(instance);

        OnInstanceGenerated?.Invoke(instance); 
    }
    #endregion
    #region Virtual Methods
    public virtual void AddInstance(T instance)
    {
        collection.Add(instance);
    }

    public virtual void RemoveInstance(T instance)
    {
        collection.Remove(instance);
    }
    #endregion
}

public class InstanceGenerator : InstanceGenerator<MonoBehaviour>
{

}



