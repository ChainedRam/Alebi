using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainedRam.Core.Generation.Extention; 

namespace ChainedRam.Core.Generation
{
    #region Enum
    public enum ConditionOption : int
    {
        Parent,
        AtLeastOne,
        All,
    }

    public enum NestedGenerateOption : int
    {
        Force,
        Individual,
    }
    #endregion

    /// <summary>
    /// NestedGenerator behaves on behalf of it's children combining their calls into one generator. 
    /// </summary>
    [Serializable]
    public class NestedGenerator : Generator
    {
        #region Custom Inspector  Attributes  
        //Custome Inspector shows this as an array only for this class excluding children. 
        /// <summary>
        /// Holds children generators 
        /// </summary>
        public virtual Generator[] ChildGenerators { get { return Generators; } set { Generators = value; } } 

        [HideInInspector]
        [SerializeField]
        private Generator[] Generators; 

        [HideInInspector()]
        public bool ShowChildrenInspecter;
        #endregion
        #region Inspecter Attributes  
        [Tooltip("'Should Generate' Condition between children.")]
        public ConditionOption ShouldTerminateOption;

        public ConditionOption ShouldGenerateOption;

        public NestedGenerateOption GenerateOption;
        #endregion
        #region Unity Methods  
        #endregion
        #region Generator Override  

        private void ForChildren(Action<Generator> act)
        {
            foreach(var gen in ChildGenerators)
            {
                act(gen); 
            }
        }

        /// <summary>
        /// And or Or's children's 'ShouldGenerate' based on selected Option
        /// </summary>
        /// <returns></returns>
        //TODO: DRY
        protected override bool ShouldGenerate()
        {
            bool baseValue = true; 

            if(ShouldGenerateOption.HasFlag(ConditionOption.Parent))
            {
                return baseValue; 
            }

            bool earlyTermination;

            Func<bool, bool, bool> operation = (earlyTermination = ShouldGenerateOption.HasFlag(ConditionOption.AtLeastOne)) ?  operation = (a, b) => a | b : operation = (a, b) => a & b;

            bool result = !earlyTermination; 
            foreach (Generator g in ChildGenerators)
            {
                bool should = GeneratorShouldGenerate(g); 

                result = operation(result, should); 

                if(should == earlyTermination)
                {
                    return earlyTermination; 
                }
            }

            return result;
        }

        //TODO: DRY
        protected override bool ShouldTerminate()
        {
            bool baseValue = false;

            if (ShouldTerminateOption.HasFlag(ConditionOption.Parent))
            {
                return baseValue;
            }
            bool earlyTermination;

            Func<bool, bool, bool> operation = (earlyTermination = ShouldTerminateOption.HasFlag(ConditionOption.AtLeastOne)) ? operation = (a, b) => a | b : operation = (a, b) => a & b;

            bool result = !earlyTermination;
            foreach (Generator g in ChildGenerators)
            {
                bool should = GeneratorShouldTerminate(g); 

                result = operation(result, should);

                if (should == earlyTermination)
                {
                    return earlyTermination;
                }
            }

            return result;
        }

        protected override void OnGenerate(GenerateEventArgs e)
        {
            //base.OnGenerate(e);

            RaiseOnGenerateEvent(); 

            foreach(var gen in ChildGenerators)
            {
                if (GenerateOption.HasFlag(NestedGenerateOption.Force) ||
                    (GenerateOption.HasFlag(NestedGenerateOption.Individual) && GeneratorShouldGenerate(gen)))
                 {
                    float prevDelta = gen.Delta;
                    gen.Delta = e.Delta;
                    gen.Generate();
                    gen.Delta = prevDelta;
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Generic.
    /// </summary>
    /// <remarks>Made this way such that NestedGenerator<T>can be treated like a non-generic NestedGenrator. And both would share a custome editor</remarks>
    public class NestedGenerator<T> : NestedGenerator where T : Generator
    {
        public T[] Children; 

        public sealed override Generator[] ChildGenerators
        {
            get
            {
                return Children; 
            }
        }
    }
}