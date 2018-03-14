﻿using ChainedRam.Core.Generation;
using ChainedRam.Core.Selection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    public class PoolGenerator : NestedGenerator
    {
        //[Header("Pool Geenerator Settings")]

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
            return !((Selected?.IsGenerating) ?? false); //TODO use GeneratorShouldGnerate //maybe not 
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