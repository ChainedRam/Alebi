using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ChainedRam.Core.Generation
{
    #region Enum
    public enum ShouldGenerateOptions : int
    {
        And, 
        Or
    }
    #endregion

    /// <summary>
    /// NestedGenerator behaves on behalf of it's children combining their calls into one generator. 
    /// </summary>
    public class NestedGenerator : Generator
    {
        #region Custom Inspector  Attributes  
        //Custome Inspector shows this as an array only for this class excluding children. 

        /// <summary>
        /// Holds children generators 
        /// </summary>
        public virtual Generator[] ChildGenerators { get; set; }
        #endregion
        #region Inspecter Attributes  
        [Header("NestedGenerator Settings")]
        [Tooltip("'Should Generate' Condition between children.")]
        public ShouldGenerateOptions Options = ShouldGenerateOptions.And;
        #endregion
        #region Unity Methods  
        private void Awake()
        {
            foreach (Generator gen in ChildGenerators)
            {
                Attach(gen); 
            }
        }
        #endregion
        #region Generator Override  
        /// <summary>
        /// And or Or's children's 'ShouldGenerate' based on selected Option
        /// </summary>
        /// <returns></returns>
        public override bool ShouldGenerate()
        {
            bool earlyTermination;

            Func<bool, bool, bool> operation = (earlyTermination = Options.HasFlag(ShouldGenerateOptions.Or))?  operation = (a, b) => a | b : operation = (a, b) => a & b;

            bool result = !earlyTermination; 
            foreach (Generator g in ChildGenerators)
            {
                bool should = g.ShouldGenerate();

                result = operation(result, should); 

                if(should == earlyTermination)
                {
                    return earlyTermination; 
                }
            }

            return result;
        }

        public override void SkippedGeneration()
        {
            foreach (Generator gen in ChildGenerators)
            {
                gen.SkippedGeneration();
            }
        }
        #endregion
    }

    /// <summary>
    /// None Generic.
    /// </summary>
    //public class NestedGenerator : NestedGenerator<Generator>
    //{ } fuck unity custom editor

}