using ChainedRam.Core.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Generation
{    
    /// <summary>
    /// A Compnent that generates. 
    /// </summary>
    public abstract class Generator : MonoBehaviour
    {
        #region Inspecter Attributes
        public float Delta;
        #endregion
        #region Private Attributes
        /// <summary>
        /// Used to count number of generate calls. 
        /// </summary>
        private int? GenerateCounter;

        /// <summary>
        /// Used to help ShouldGenerate with build-in timer for generation.
        /// </summary>
        private float? ShouldGenerateWaitTime;

        /// <summary>
        /// Used to help ShouldTerminate with build-in timer for termination. 
        /// </summary>
        private float? ShouldTerminateWaitTime;

        /// <summary>
        /// Used to know if generator has skipped at least once. 
        /// </summary>
        private bool HasSkipped;
        #endregion
        #region Public Events 
        /// <summary>
        /// Invoked when generating. <see cref="Generate"/>
        /// </summary>
        public EventHandler<GenerateEventArgs> OnGenerateEventHandler;

        /// <summary>
        /// Invoked when stoping generation. <see cref="DoSkippedGenerate"/>
        /// </summary>
        public EventHandler<GenerateEventArgs> OnSkippedEventHandler;

        /// <summary>
        /// Invoked when Starting generation. <see cref="Begin"/>
        /// </summary>
        public EventHandler<GenerateEventArgs> OnBeginEventHandler;

        /// <summary>
        /// Invoked when stoping generation. <see cref="End"/>
        /// </summary>
        public EventHandler<GenerateEventArgs> OnEndEventHandler;
        #endregion
        #region Raise Event
        /// <summary>
        /// Raises a generation event
        /// </summary>
        /// <param name="genEvent"></param>
        protected void Raise(EventHandler<GenerateEventArgs> genEvent)
        {
            genEvent?.Invoke(this, new GenerateEventArgs(Delta));
        }

        /// <summary>
        /// Raise OnGenerateEvent
        /// </summary>
        protected void RaiseOnGenerateEvent()
        {
            Raise(OnGenerateEventHandler);
        }

        /// <summary>
        /// Raise OnSkippedEvent
        /// </summary>
        protected void RaiseOnSkippedEvent()
        {
            Raise(OnSkippedEventHandler);
        }

        /// <summary>
        /// Raise OnBeginEvent
        /// </summary>
        protected void RaiseOnBeginEvent()
        {
            Raise(OnBeginEventHandler);
        }

        /// <summary>
        /// aise OnEndEvent
        /// </summary>
        protected void RaiseOnEndEvent()
        {
            Raise(OnEndEventHandler);
        }
        #endregion
        #region Public Methods 
        /// <summary>
        /// Generates something. 
        /// </summary>
        public void Generate()
        {
            OnGenerate(new GenerateEventArgs(Delta));
            RaiseOnGenerateEvent();
        }

        /// <summary>
        /// Called when generate call skips a cycle because <see cref="ShouldGenerate"/> returns false. 
        /// </summary>
        public void SkippedGenerate()
        {
            OnSkip();
            RaiseOnSkippedEvent();
        }

        /// <summary>
        /// Begins Generations. Does not throw exception if already generating. 
        /// </summary>
        public void Begin()
        {
            enabled = true;
        }

        /// <summary>
        /// Begins Generation and invokes OnStart events.
        /// </summary>
        public void BeginSafely()
        {
            if (enabled == false)
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
            enabled = false;
        }

        /// <summary>
        /// Stops Generation and trigger OnStop events.
        /// </summary>
        public void EndSafely(bool safe = false)
        {
            if (enabled == false && safe)
            {
                throw new GeneratorEndException("Generator is already not genrating");
            }

            End();
        }
        #endregion
        #region Unity Methods 
        /// <summary>
        /// More organized way to call Awake since this call wil lbe inhereted. 
        /// </summary>
        protected virtual void Awake()
        {

        }

        /// <summary>
        /// More organized way to call Start since this call wil lbe inhereted. 
        /// </summary>
        protected virtual void Start()
        {

        }

        /// <summary>
        /// Runs the generation cycle.  DO NOT OVERWRITE
        /// </summary>
        protected virtual void FixedUpdate()
        {
            if (ShouldTerminate())
            {
                End();
                return;
            }

            TickHelperProtectedMethods();

            if (ShouldGenerate())
            {
                Generate();
                TickHelperGenerateMethods();
            }
            else
            {
                SkippedGenerate();
                TickHelperSkippedGenerate();
            }
        }

       
        /// <summary>
        /// Called when componetn is enabled; whenever <see cref="Behaviour.enabled"/> flag is set to true.  
        /// </summary>
        protected void OnEnable()
        {
            OnBegin();
            RaiseOnBeginEvent();
            ResetHelperAttributes(); 
        }

        /// <summary>
        /// Called when componetn is disabled; whenever <see cref="Behaviour.enabled"/> flag is set to false.  
        /// </summary>
        protected void OnDisable()
        {
            OnEnd();
            RaiseOnEndEvent();
        }
        #endregion
        #region Protected Methods
        /// <summary>
        /// Returns true only once per run. 
        /// </summary>
        /// <returns></returns>
        protected bool ShouldGenerateOnce()
        {
            return ShouldGenerateCount(1);
        }

        /// <summary>
        /// Returns true n given times. 
        /// </summary>
        /// <returns></returns>
        protected bool ShouldGenerateCount(int n)
        {
            GenerateCounter = GenerateCounter ?? n;
            return GenerateCounter > 0; 
        }

        /// <summary>
        /// Returns true after waiting for given seconds. 
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        protected bool ShouldGenerateWait(float seconds)
        {
            ShouldGenerateWaitTime = ShouldGenerateWaitTime ?? seconds;

            return ShouldGenerateWaitTime <= 0.001f; //Define constant
        }

        /// <summary>
        /// Returns true after waiting for given seconds. 
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        protected bool ShouldTerminateWait(float seconds)
        {
            ShouldTerminateWaitTime = ShouldTerminateWaitTime ?? seconds;

            return ShouldTerminateWaitTime <= 0.001f; //Define constant
        }

        /// <summary>
        /// Returns true if generator has skipped at least once. 
        /// </summary>
        /// <returns></returns>
        protected bool ShouldTerminateOnSkippedGeneration()
        {
            return HasSkipped; 
        }

        #endregion
        #region Protected Abstract
        /// <summary>
        /// Called within <see cref="FixedUpdate"/> to check if generation is allowed.
        /// </summary>
        /// <returns></returns>
        protected abstract bool ShouldGenerate();

        /// <summary>
        /// Called within <see cref="FixedUpdate"/> to check if generation shoul terminate before generating.
        /// </summary>
        /// <returns></returns>
        protected abstract bool ShouldTerminate();

        /// <summary>
        /// Called when genrator generates. This is invoked before <see cref="OnGenerateEventHandler"/> 
        /// </summary>
        protected abstract void OnGenerate(GenerateEventArgs e);

        #endregion
        #region Protected Virtual Methods
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
        #region Protected Static Methods 
        /// <summary>
        /// Gets whether a generator should generate or not. Is is used for nested generators. 
        /// </summary>
        /// <param name="gen"></param>
        /// <returns></returns>
        protected static bool GeneratorShouldGenerate(Generator gen)
        {
            return gen.ShouldGenerate();
        }

        /// <summary>
        /// Gets whether a generator should terminate or not. Is is used for nested generators. 
        /// </summary>
        /// <param name="gen"></param>
        /// <returns></returns>
        protected static bool GeneratorShouldTerminate(Generator gen)
        {
            return gen.ShouldTerminate();
        }
        #endregion
        #region Private Methods

        /// <summary>
        /// Resets Helper attributes for reuse. 
        /// </summary>
        private void ResetHelperAttributes()
        {
            GenerateCounter = null; 
            ShouldTerminateWaitTime = null;
            ShouldGenerateWaitTime = null;
            HasSkipped = false; 
        }

        /// <summary>
        /// Called every update 
        /// </summary>
        private void TickHelperProtectedMethods()
        {
            UpdateWaitTime(ref ShouldTerminateWaitTime);
            UpdateWaitTime(ref ShouldGenerateWaitTime);
        }

        /// <summary>
        /// Reduce GenerateCounter for ShouldGenerateTimes method. 
        /// </summary>
        private void TickHelperGenerateMethods()
        {
            if (GenerateCounter.HasValue)
            {
                GenerateCounter--;
            }
        }

        private void TickHelperSkippedGenerate()
        {
            HasSkipped = true;
        }

        private void UpdateWaitTime(ref float? WaitTime)
        {
            if (WaitTime.HasValue && WaitTime > 0)
            {
                WaitTime -= Time.fixedDeltaTime;
            }
        } 


        #endregion
    }
}
