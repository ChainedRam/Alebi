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

        public int Repeat = 1;

        public Generator[] Generators;

        public Generator Selected;

       
        [HideInInspector]
        public SelectorType SelectorType;

        private bool HasRanOut = false;
        private int Index;

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
            //Selected = null;
        }

        protected override void OnBegin()
        {
            HasRanOut = false;
            ResetSelector();
            Next();
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
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override bool ShouldGenerate()
        {
            return false;
        }

        protected override bool ShouldTerminate()
        {
            return HasRanOut; 
        }

        public Generator NextGenerator()
        {
            return Select(ChildGenerators, Selected);
        }

        protected override void OnEnd()
        {
            if (Selected != null)
            {
                Demote(Selected);
            }
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

        public T Select<T>(T[] list, T prev = null) where T : class
        {
            if (list.Length == 0)
            {
                throw new Exception("Cannot select from empty list"); //TODO custom exception
            }

            if (Repeat > 0 && Index + 1 >= list.Length)
            {
                Repeat--;
                Index = 0;
            }

            if (Index < list.Length)
            {
                return list[(Index++)];
            }
            else
            {
                return null;
            }
        }

        public void ResetSelector()
        {
            Index = 0;
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