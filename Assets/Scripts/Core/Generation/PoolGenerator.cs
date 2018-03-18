using ChainedRam.Core.Generation;
using ChainedRam.Core.Selection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    public class PoolGenerator : Generator
    {
        public virtual Generator[] ChildGenerators { get { return Generators; } set { Generators = value; } }

        public Generator[] Generators;

        [HideInInspector()]
        public Generator Selected;

        [HideInInspector()]
        public Selector Selector;

        [HideInInspector]
        public SelectorType SelectorType;

        private bool HasRanOut = false; 

        public void SwitchIn(Generator gen)
        {
            if (Selected != null)
            {
                Demote(Selected);
            }

            if (gen != null)
            {
                Promote(gen);
            }
        }

        public void Promote(Generator gen)
        {
            Selected = gen;

            gen.OnEndEventHandler += Next;

            gen.Delta = this.Delta; 
            gen.Begin();
        }

        public void Demote(Generator gen)
        {
            gen.OnEndEventHandler -= Next;
            Selected = null;
        }

        protected override void OnBegin()
        {
            HasRanOut = false;
            Selector.ResetSelector(); 
        }

        private void Next(object s= null, GenerateEventArgs e= null)
        {
            Generator nextGen = NextGenerator(); 

            if(nextGen == null)
            {
                HasRanOut = true; 
            }
            else
            {
                SwitchIn(nextGen);
                
            }
        }

        protected override void OnGenerate(GenerateEventArgs e)
        {
            Next();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override bool ShouldGenerate()
        {
            return !((Selected?.enabled) ?? false); //TODO use GeneratorShouldGnerate //maybe not 
        }

        protected override bool ShouldTerminate()
        {
            return HasRanOut; 
        }

        public Generator NextGenerator()
        {
            return Selector.Select(ChildGenerators, Selected);
        }

        protected override void OnEnd()
        {
            if(Selected != null)
                Demote(Selected);
        }

        [ContextMenu("From Children")]
        private void SetGeneratorsFromChildren()
        {
            List<Generator> gens = new List<Generator>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Generator g = transform.GetChild(i).GetComponent<Generator>();

                if(g!= null)
                {
                    gens.Add(g); 
                }
            }

            ChildGenerators = gens.ToArray(); 
        }
    }

    /// <summary>
    /// Generic version of PoolGenerator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PoolGenerator<T> : PoolGenerator where T : Generator
    {
        public T[] Pool;

        public override Generator[] ChildGenerators
        {
            get
            {
                return Pool;
            }
        }

    }
}