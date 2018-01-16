using ChainedRam.Core.Generation;
using ChainedRam.Core.Selection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    public class PoolGenerator : NestedGenerator
    {
        [Header("Pool Geenerator Settings")]

        [HideInInspector()]
        public Generator Selected;

        [HideInInspector()]
        public Selector Selector;

        [HideInInspector]
        public SelectorType SelectorType;

        public void SwitchIn(Generator gen)
        {
            if (Selected != null)
            {
                Demote(Selected);
            }

            Promote(gen);
        }

        public void Promote(Generator gen)
        {
            // Debug.Log("Promoting " + gen.name);

            Selected = gen;
            //Attach(gen);

            gen.Delta = this.Delta; 
            gen.Begin();
        }

        public void Demote(Generator gen)
        {
            //Debug.Log("Demoting " + gen.name);
            gen.End();
            //Detach(gen);

            Selected = null;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnAwake()
        {

        }

        protected override void OnGenerate(GenerateEventArgs e)
        {
            Delta = e.Delta; 
            SwitchIn(NextGenerator());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override bool ShouldGenerate()
        {
            return !((Selected?.IsGenerating) ?? false);
        }

        protected override void OnSkip()
        {
            base.OnSkip();
        }

        public Generator NextGenerator()
        {
            return Selector.Select(ChildGenerators, Selected);
        }

        protected override void OnEnd()
        {
            Demote(Selected);
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