
namespace ChainedRam.Core.Generation.Extention
{

    public static class GeneratorExtentions
    {
        /// <summary>
        /// Adds follower's as a listener when Start. 
        /// </summary>
        /// <param name="follower"></param>
        /// <remarks>To avoid recursion, followe <see cref="DetachOnStart(this Generator source, Generator)"/> the calling generator. </remarks>
        public static void AttachOnBegin(this Generator source, Generator follower)
        {
            //avoid recursion 
            follower.DetachOnBegin(source);

            source.OnBeginEventHandler += (e, a) => follower.Begin();
        }

        /// <summary>
        /// Removes following's StartGenerating from Start event list. 
        /// </summary>
        /// <param name="follower"></param>
        public static void DetachOnBegin(this Generator source, Generator follower)
        {
            source.OnBeginEventHandler -= (e, a) => follower.Begin();
        }

        /// <summary>
        /// Adds follower as listern when stop. 
        /// </summary>
        /// <param name="follower"></param>
        public static void AttachOnEnd(this Generator source, Generator follower)
        {
            //avoid recursion 
            follower.DetachOnEnd(source);

            source.OnEndEventHandler -= (e, a) => follower.End();
        }

        /// <summary>
        /// Removes follower from Stop event list. 
        /// </summary>
        /// <param name="follower"></param>
        public static void DetachOnEnd(this Generator source, Generator follower)
        {
            source.OnEndEventHandler -= (e, a) => follower.End();
        }

        /// <summary>
        /// Attaches follower to genrate event. 
        /// </summary>
        /// <param name="follower"></param>    
        public static void AttachOnGenerate(this Generator source, Generator follower)
        {
            //avoid recursion 
            follower.DetachOnGenerate(source);

            //avoid duplicate
            DetachOnGenerate(source, follower);

            source.OnGenerateEventHandler += (e, a) => follower.Generate();
        }

        /// <summary>
        /// Removes follower from on genrate event list. 
        /// </summary>
        /// <param name="follower"></param>
        public static void DetachOnGenerate(this Generator source, Generator follower)
        {
            source.OnGenerateEventHandler -= (e, a) => follower.Generate();
        }

        /// <summary>
        /// Attaches follower to genrate event. 
        /// </summary>
        /// <param name="follower"></param>    
        public static void AttachOnSkipped(this Generator source, Generator follower)
        {
            //avoid recursion 
            follower.DetachOnSkipped(source);

            source.OnSkippedEventHandler += (e, a) => follower.SkippedGenerate();
        }

        /// <summary>
        /// Removes follower from on genrate event list. 
        /// </summary>
        /// <param name="follower"></param>
        public static void DetachOnSkipped(this Generator source, Generator follower)
        {
            source.OnSkippedEventHandler -= (e, a) => follower.SkippedGenerate();
        }

        /// <summary>
        /// Attachs follower's OnStart, OnStop,OnGenerate, & OnSkip events to own events, 
        /// </summary>
        /// <param name="follower"></param>
        public static void Attach(this Generator source, Generator follower)
        {
            AttachOnGenerate(source, follower);
            AttachOnSkipped(source, follower);
            AttachOnBegin(source, follower);
            AttachOnEnd(source, follower);
        }

        /// <summary>
        /// Detaches follower's OnStart, OnStop,OnGenerate, & OnSkip events from own events, 
        /// </summary>
        /// <param name="follower"></param>
        public static void Detach(this Generator source, Generator follower)
        {
            DetachOnGenerate(source, follower);
            DetachOnSkipped(source, follower);
            DetachOnBegin(source, follower);
            DetachOnEnd(source, follower);
        }
    }
}

