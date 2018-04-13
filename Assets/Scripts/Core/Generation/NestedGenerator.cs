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
    public class NestedGenerator : TimedGenerator
    {
        #region Custom Inspector  Attributes  
        //Custome Inspector shows this as an array only for this class excluding children. 
        /// <summary>
        /// Holds children generators 
        /// </summary>
        public virtual Generator[] ChildGenerators { get { return Generators; } set { Generators = value; } }
        [Header("Nested Generator")]

        [SerializeField]
        private Generator[] Generators; 

        #endregion
        #region Generator Override  

        private void ForChildren(Action<Generator> act)
        {
            foreach(var gen in ChildGenerators)
            {
                act(gen); 
            }
        }

        protected override bool ShouldGenerate()
        {
            return true; 
        }

        protected override void OnGenerate(GenerateEventArgs e)
        {
           //generate independetly 
        }

        protected override void OnBegin()
        {
            base.OnBegin();
            ForChildren(c => c.Begin());
        }
        
        protected override void OnEnd()
        {
            base.OnEnd();
            ForChildren(c => c.End()); 
        }

        [ContextMenu("From Children")]
        private void SetGeneratorsFromChildren()
        {
            List<Generator> gens = new List<Generator>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Generator g = transform.GetChild(i).GetComponent<Generator>();

                if (g != null)
                {
                    gens.Add(g);
                }
            }

            ChildGenerators = gens.ToArray();
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