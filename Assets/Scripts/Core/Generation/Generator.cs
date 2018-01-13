using ChainedRam.Core.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace ChainedRam.Core.Generation
{
    [Flags]
    public enum TerminationType
    {
        Internal,
        External,
        Duration,
        Counter,
        OnSkip
    }

    [Flags]
    public enum GenerationType { Internal, External, Cooldown }
    /// <summary>
    /// A Compnent that generates. 
    /// </summary>
    public abstract class Generator : MonoBehaviour
    {
        #region Inspector Attributes

        [HideInInspector()]
        public bool IsGenerating;

        [HideInInspector()]
        public bool PrintDebug;

        [HideInInspector()]
        public TerminationType TerminatorTag = TerminationType.Internal;

        [HideInInspector()]
        public GenerationType GenerateConditionTag = GenerationType.Internal;

        [HideInInspector()]
        public GeneratorTerminator Terminator;

        [HideInInspector()]
        public GeneratorCondition GenerateCondition;


        #endregion
        #region Public Events 
        /// <summary>
        /// Invoked when generating. <see cref="Generate"/>
        /// </summary>
        public EventHandler OnGenerateEventHandler;

        /// <summary>
        /// Invoked when stoping generation. <see cref="DoSkippedGenerate"/>
        /// </summary>
        public EventHandler OnSkippedEventHandler;

        /// <summary>
        /// Invoked when Starting generation. <see cref="Begin"/>
        /// </summary>
        public EventHandler OnBeginEventHandler;

        /// <summary>
        /// Invoked when stoping generation. <see cref="End"/>
        /// </summary>
        public EventHandler OnEndEventHandler;

        #endregion
        #region Public Methods 
        /// <summary>
        /// Generates something. 
        /// </summary>
        public void Generate()
        {
            OnGenerate();
            OnGenerateEventHandler?.Invoke(this, EventArgs.Empty); 
        }

        /// <summary>
        /// Called when generate call skips a cycle because <see cref="ShouldGenerate"/> returns false. 
        /// </summary>
        public void SkippedGenerate()
        {
            OnSkip();
            OnSkippedEventHandler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Begins Generations. Does not throw exception if already generating. 
        /// </summary>
        public void Begin()
        {
            IsGenerating = true;
            OnBegin();
            OnBeginEventHandler?.Invoke(this, EventArgs.Empty);

            GenerateCondition?.Setup(this);
            Terminator?.Setup(this);
        }

        /// <summary>
        /// Begins Generation and invokes OnStart events.
        /// </summary>
        public void BeginSafly()
        {
            if (IsGenerating == false)
            {
                throw new GeneratorBeginException("Generator is already genrating");
            }

            Begin(); 
        }

        /// <summary>
        /// Ends Generations. Does not throw exception if already stopped. 
        /// </summary>
        public void End()
        {
            IsGenerating = false;
            OnEnd();
            OnEndEventHandler?.Invoke(this, EventArgs.Empty);

            GenerateCondition?.SetApart(this);
            Terminator?.SetApart(this);
        }
       
        /// <summary>
        /// Stops Generation and trigger OnStop events.
        /// </summary>
        public void EndSafly(bool safe = false)
        {
            if (IsGenerating == false && safe)
            {
                throw new GeneratorEndException("Generator is already not genrating");
            }

            End(); 
        }
        #endregion
        #region Unity Methods 
        /// <summary>
        /// 'Seals' Awake function. DO NOT OVERWRITE! use <see cref="OnAwake"/>
        /// </summary>
        protected void Awake()
        {
            OnAwake();
            Terminator?.Setup(this);
            GenerateCondition?.Setup(this);
        }

        /// <summary>
        /// 'Seals' Start function. DO NOT OVERWRITE. use <see cref="Start"/>
        /// </summary>
        protected void Start()
        {
            OnStart();
        }

        /// <summary>
        /// Runs the generation cycle.  DO NOT OVERWRITE
        /// </summary>
        protected void Update()
        {
            if (IsGenerating == false)
            {
                return;
            }

            if (Terminator?.ShouldTerminate(this) ?? ShouldTerminate())
            {
                End();
                return;
            }

            if (GenerateCondition?.ShouldGenerate(this) ?? ShouldGenerate())
            {
                Generate();
            }
            else
            {
                SkippedGenerate();
            }
        }
        #endregion
        #region Protected Virtual Methods
        /// <summary>
        /// Called within <see cref="Update"/> to check if generation is allowed.
        /// </summary>
        /// <returns></returns>
        protected virtual bool ShouldGenerate() => true;

        /// <summary>
        /// Called within <see cref="Update"/> to check if generation shoul terminate before generating.
        /// </summary>
        /// <returns></returns>
        protected virtual bool ShouldTerminate() => false; 

        /// <summary>
        /// Gets called when this object's Awake function. 
        /// </summary>
        protected virtual void OnAwake() { }

        /// <summary>
        /// Gets called when this object's Awake function. 
        /// </summary>
        protected virtual void OnStart() { }

        /// <summary>
        /// Called when genrator generates. This is invoked before <see cref="OnGenerateEventHandler"/> 
        /// </summary>
        protected virtual void OnGenerate() { }

        /// <summary>
        /// Called when genrator skips generate. This is invoked before <see cref="OnSkippedEventHandler"/> 
        /// </summary>
        protected virtual void OnSkip() { }

        /// <summary>
        /// Called when genrator begins generating. This is invoked before <see cref="OnBeginEventHandler"/> 
        /// </summary>
        protected virtual void OnBegin() { }

        /// <summary>
        /// Called when genrator ends generating. This is invoked before <see cref="OnEndEventHandler"/> 
        /// </summary>
        protected virtual void OnEnd() { }
        #endregion
    }
}

