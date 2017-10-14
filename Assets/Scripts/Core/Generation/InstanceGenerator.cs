using System;
using System.Collections;
using System.Collections.Generic;
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
    protected abstract IEnumerable<T> Instances { get; }
    #endregion
    #region Unity Methods
    private void Awake()
    {
        OnGenerate += GenerateInstance;
    }
    #endregion
    #region Public Events
    /// <summary>
    /// Called after generating an instance. 
    /// </summary>
    public event Action<T> OnInstanceGenerated;
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
        return Instantiate(Prefab, GenerateAt.transform);
    }
   
    public virtual void SetupGenerated(T instance)
    {
        instance.gameObject.SetActive(true);
        instance.transform.localPosition = Vector2.zero;
        instance.transform.rotation = Quaternion.identity; 
    }
    #endregion
    #region Abstarct Methods
    public abstract void AddInstance(T instance);
    public abstract void RemoveInstance(T instance);
    #endregion
}
