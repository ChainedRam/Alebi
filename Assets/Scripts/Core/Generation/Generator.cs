using ChainedRam.Core.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace ChainedRam.Core.Generation
{
    [Flags]
    public enum TerminationType { Internal, External, Duration, Counter, OnSkip }
    [Flags]
    public enum GeneratingType { Internal, External, Cooldown }
    /// <summary>
    /// A Compnent that generates. 
    /// </summary>
    public class Generator : MonoBehaviour
    {
        #region Inspector Attributes

        [HideInInspector()]
        public bool IsGenerating;

        [HideInInspector()]
        public bool PrintDebug;

        [HideInInspector()]
        public TerminationType TerminatorTag = TerminationType.Internal;

        [HideInInspector()]
        public GeneratingType GenerateConditionTag = GeneratingType.Internal;

        [HideInInspector()]
        public GeneratorTerminator Terminator;

        [HideInInspector()]
        public GeneratorCondition GenerateCondition;


        #endregion
        #region Public Events 
        /// <summary>
        /// Invoked when generating. <see cref="Generate"/>
        /// </summary>
        public event Action OnGenerate;

        /// <summary>
        /// Invoked when stoping generation. <see cref="DoSkippedGenerate"/>
        /// </summary>
        public event Action OnSkippedGenerate;

        /// <summary>
        /// Invoked when Starting generation. <see cref="BeginGenerating"/>
        /// </summary>
        public event Action OnBeginGenerating;

        /// <summary>
        /// Invoked when stoping generation. <see cref="EndGenerating"/>
        /// </summary>
        public event Action OnEndGenerating;

        #endregion
        #region Public Methods 
        /// <summary>
        /// Generates something. 
        /// </summary>
        public void Generate()
        {
            WhenGenerate();
            OnGenerate?.Invoke();
        }

        /// <summary>
        /// Called when generate call skips a cycle because <see cref="ShouldGenerate"/> returns false. 
        /// </summary>
        private void SkippedGenerate()
        {
            WhenSkipped();
            OnSkippedGenerate?.Invoke();
        }

        /// <summary>
        /// Begins Generations. Does not throw exception if already generating. 
        /// </summary>
        public void BeginGenerating()
        {
            BeginGenerating(false);
        }

        /// <summary>
        /// Begins Generation and invokes OnStart events.
        /// </summary>
        public void BeginGenerating(bool safe = false)
        {
            if (safe && IsGenerating == false && safe)
            {
                throw new GeneratorBeginException("Generator is already genrating");
            }
            IsGenerating = true;
            WhenBegin();
            OnBeginGenerating?.Invoke();

            GenerateCondition?.Setup(this);
            Terminator?.Setup(this);
        }

        /// <summary>
        /// Ends Generations. Does not throw exception if already stopped. 
        /// </summary>
        public void EndGenerating()
        {
            EndGenerating(false);
        }
        /// <summary>
        /// Stops Generation and trigger OnStop events.
        /// </summary>
        public void EndGenerating(bool safe = false)
        {
            if (IsGenerating == false && safe)
            {
                throw new GeneratorEndException("Generator is already not genrating");
            }

            IsGenerating = false;
            WhenEnd();
            OnEndGenerating?.Invoke();

            GenerateCondition?.SetApart(this);
            Terminator?.SetApart(this);
        }
        #region Attach Detach Methods
        /// <summary>
        /// Adds follower's as a listener when Start. 
        /// </summary>
        /// <param name="follower"></param>
        /// <remarks>To avoid recursion, followe <see cref="DetachOnStart(Generator)"/> the calling generator. </remarks>
        public void AttachOnStart(Generator follower)
        {
            //avoid recursion 
            follower.DetachOnStart(this);

            OnBeginGenerating += follower.BeginGenerating;
        }

        /// <summary>
        /// Removes following's StartGenerating from Start event list. 
        /// </summary>
        /// <param name="follower"></param>
        public void DetachOnStart(Generator follower)
        {
            OnBeginGenerating -= follower.BeginGenerating;
        }

        /// <summary>
        /// Adds follower as listern when stop. 
        /// </summary>
        /// <param name="follower"></param>
        public void AttachOnStop(Generator follower)
        {
            //avoid recursion 
            follower.DetachOnStop(this);

            OnEndGenerating += follower.EndGenerating;
        }

        /// <summary>
        /// Removes follower from Stop event list. 
        /// </summary>
        /// <param name="follower"></param>
        public void DetachOnStop(Generator follower)
        {
            OnEndGenerating -= follower.EndGenerating;
        }

        /// <summary>
        /// Attaches follower to genrate event. 
        /// </summary>
        /// <param name="follower"></param>    
        public void AttachOnGenerate(Generator follower)
        {
            //avoid recursion 
            follower.DetachOnGenerate(this);

            //avoid duplicate
            DetachOnGenerate(follower);

            OnGenerate += follower.Generate;
        }

        /// <summary>
        /// Removes follower from on genrate event list. 
        /// </summary>
        /// <param name="follower"></param>
        public void DetachOnGenerate(Generator follower)
        {
            OnGenerate -= follower.Generate;
        }

        /// <summary>
        /// Attaches follower to genrate event. 
        /// </summary>
        /// <param name="follower"></param>    
        public void AttachOnSkip(Generator follower)
        {
            //avoid recursion 
            follower.DetachOnSkip(this);

            OnSkippedGenerate += follower.SkippedGenerate;
        }

        /// <summary>
        /// Removes follower from on genrate event list. 
        /// </summary>
        /// <param name="follower"></param>
        public void DetachOnSkip(Generator follower)
        {
            OnSkippedGenerate -= follower.SkippedGenerate;
        }

        /// <summary>
        /// Attachs follower's OnStart, OnStop,OnGenerate, & OnSkip events to own events, 
        /// </summary>
        /// <param name="follower"></param>
        public void Attach(Generator follower)
        {
            AttachOnGenerate(follower);
            AttachOnSkip(follower);
            AttachOnStart(follower);
            AttachOnStop(follower);
        }

        /// <summary>
        /// Detaches follower's OnStart, OnStop,OnGenerate, & OnSkip events from own events, 
        /// </summary>
        /// <param name="follower"></param>
        public void Detach(Generator follower)
        {
            DetachOnGenerate(follower);
            DetachOnSkip(follower);
            DetachOnStart(follower);
            DetachOnStop(follower);
        }
        #endregion
        #endregion
        #region Unity Methods 

        /// <summary>
        /// Seals Awake function
        /// </summary>
        protected void Awake()
        {
            WhenAwake();
            Terminator?.Setup(this);
            GenerateCondition?.Setup(this);
        }

        /// <summary>
        /// Seals Start function
        /// </summary>
        protected void Start()
        {
            WhenStart();
        }

        /// <summary>
        /// Runs the generation cycle. 
        /// </summary>
        protected void Update()
        {
            if (IsGenerating == false)
            {
                return;
            }

            if (Terminator?.ShouldTerminate(this) ?? ShouldTerminate())
            {
                EndGenerating();
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
        protected virtual bool ShouldGenerate() { return true; }

        /// <summary>
        /// Called within <see cref="Update"/> to check if generation shoul terminate before generating.
        /// </summary>
        /// <returns></returns>
        protected virtual bool ShouldTerminate() { return false; }


        /// <summary>
        /// Gets called when this object's Awake function. 
        /// </summary>
        protected virtual void WhenAwake() { }

        /// <summary>
        /// Gets called when this object's Awake function. 
        /// </summary>
        protected virtual void WhenStart() { }

        /// <summary>
        /// Called when genrator generates. This is invoked before <see cref="OnGenerate"/> 
        /// </summary>
        protected virtual void WhenGenerate() { }

        /// <summary>
        /// Called when genrator skips generate. This is invoked before <see cref="OnSkippedGenerate"/> 
        /// </summary>
        protected virtual void WhenSkipped() { }

        /// <summary>
        /// Called when genrator begins generating. This is invoked before <see cref="OnBeginGenerating"/> 
        /// </summary>
        protected virtual void WhenBegin() { }

        /// <summary>
        /// Called when genrator ends generating. This is invoked before <see cref="OnEndGenerating"/> 
        /// </summary>
        protected virtual void WhenEnd() { }


        #endregion
    }
}
