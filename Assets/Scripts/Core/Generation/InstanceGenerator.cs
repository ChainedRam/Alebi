using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    /// <summary>
    /// A generator that generates components. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InstanceGenerator<T> : TimedGenerator where T : Component
    {
        #region Public Attributes
        public InstancePooler<T> InstancePooler { private get; set; }
        #endregion
        #region Inspector Attribute 
        [Header("Instance Generator")]
        [Tooltip("Prefab to genrate.")]
        public T Prefab;

        [Tooltip("Transform's location.")]
        public PositionProvider GenerateAt;

        [Tooltip("Makes generated instances children of.")]
        public Transform Parent;

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
            foreach (T i in Instances)
            {
                Degenerate(i);
            }
        }

        protected virtual void Degenerate(T instance)
        {
            RemoveInstance(instance);
            Destroy(instance.gameObject);
        }

        private T CreateInstance()
        {
            T instance = null;

            if (InstancePooler != null)
            {
                instance = InstancePooler.GetInstance();
            }
            else
            { 
                instance = Instantiate(Prefab);
            }

            if(Parent != null)
            {
                instance.transform.SetParent(Parent); 
            }

            return instance; 

        }

        public virtual void SetupGenerated(T instance)
        {
            instance.gameObject.SetActive(true);
            instance.transform.position = GenerateAt.ProvidedPosition;

            if (GenerateAt.ProvidedRotation.HasValue)
            {
                instance.transform.eulerAngles = Vector3.forward * GenerateAt.ProvidedRotation.Value;
            }
        }
        #endregion
        #region Override Generator
        protected override void Awake()
        {
            base.Awake();
            collection = new Collection<T>(); 
        }

        protected override void OnGenerate(GenerateEventArgs e)
        {
            GenerateInstance(); 
        }

        protected override void OnBegin()
        {
            base.OnBegin();
            GenerateAt.ResetRNG(); 
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        protected virtual void GenerateInstance()
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
}

