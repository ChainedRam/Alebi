using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using ChainedRam.Core.Exceptions;

namespace ChainedRam.Core.Configuration
{
    /// <summary>
    /// Configures componets in scene defined by <see cref="ConfigMapper"/>
    /// </summary>
    public sealed class ConfigureManager : MonoBehaviour
    {
        #region Singleton
        /// <summary>
        /// Holds a singleton instance of configuration manager
        /// </summary>
        public static ConfigureManager Instance { get; private set; }
        #endregion
        #region Private Members
        /// <summary>
        /// Holds maps for reuse. 
        /// </summary>
        private Dictionary<Type, ConfigMapper> Maps;
        #endregion
        #region Unity Events
        /// <summary>
        /// Insured a single instance is instantiated.
        /// </summary>
        private void Awake()
        {
            EnsureSingleton();
            ConfigureAll();
        }

        /// <summary>
        /// Insure only one instance exist in the scene
        /// </summary>
        private void EnsureSingleton()
        {
            if (Instance != null)
            {
                Debug.LogError("ConfigureManager already exist.", Instance);
                Debug.LogError("Duplicate ConfigureManager instance.", this);

                throw new DuplicateManagerException("There can only be one ConfigureManager in the scene");
            }
            Instance = this;
        }

        /// <summary>
        /// Apply mappings to all components within the scene. 
        /// </summary>
        private void ConfigureAll()
        {
            Maps = new Dictionary<Type, ConfigMapper>();

            ConfigMapper[] Mappers = FindObjectsOfType<ConfigMapper>();

            foreach (var mapper in Mappers)
            {
                mapper.Prepare(); 
                Type targetType = mapper.TargetType;
                if (Maps.ContainsKey(targetType))
                {
                    Debug.LogError($"Config Failed. Type '{targetType.FullName}' has more than two configations defined.", mapper);
                    return;  //TODO throw exception 
                }
                Maps.Add(targetType, mapper);

                foreach (var t in FindObjectsOfType(targetType) as Component[])
                {
                    mapper.Configure(t);
                }
            }
        }
        #endregion
        #region Static Methods
        /// <summary>
        /// Configure object only if an existing mapper is defined. 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="comp"></param>
        public static void Configure(Type t, Component comp)
        {
            if (Instance.Maps.ContainsKey(t) == false)
            {
                Debug.LogError($"{t.FullName} has no defined configuration");
            }

            Instance.Maps[t].Configure(comp);
        }

        /// <summary>
        /// Configure object only if an existing mapper is defined. 
        /// </summary>
        /// <typeparam name="T">Type to be configured as</typeparam>
        /// <param name="comp"></param>
        public static void Configure<T>(T comp, object hideUnityEvent = null) where T : Component
        {
            Configure(typeof(T), comp);
        }
        #endregion
        #region Inspecter Methods
        /// <summary>
        /// Configures components within game object. 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="comp"></param>
        public static void ConfigureGameObject(GameObject go, bool mustConfig = false)
        {
            EnsureInstanceExist(); 
            int configCount = 0;
            foreach (var comp in go.GetComponents<Component>())
            {
                if (Instance.Maps.ContainsKey(comp.GetType()))
                {
                    configCount++;
                    Instance.Maps[comp.GetType()].Configure(comp);
                }
            }

            if (mustConfig && configCount == 0)
            {
                Debug.LogError("Failed to configure: '" + go.name + "': object contains to confiurable components.");
            }
        }
        #endregion

        private static void EnsureInstanceExist()
        {
            if (Instance == null)
            {
                Debug.LogError("Missing ConfigureManager Instance to the scene."); 
            }
        }
    } 
}
