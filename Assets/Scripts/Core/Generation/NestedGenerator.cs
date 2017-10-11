using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ChainedRam.Core.Generation
{
    /// <summary>
    /// Generation Flags. Groups are And'ed by default. 
    /// </summary>
    internal enum GF
    {
        IncludeParent = 1,
        OrParent = 2,
        IncludeChildren = 4,
        OrChildren = 8
    }

    public enum ShouldGenerateOptions : int
    {
        ///1 -> include parent  
        ///2 -> and parent 
        ///4 -> include children
        ///8 -> and children 
        /// [0,0,0,0]
        /// [1,2,4,8]
        ParentOnly = GF.IncludeParent | GF.OrParent,
        OneChild = GF.IncludeChildren | GF.OrChildren | GF.OrParent,
        AllChildren = GF.IncludeChildren | GF.OrParent,

        ParnetOrOneChild = GF.IncludeParent | GF.IncludeChildren | GF.OrParent,
        ParnetAndOneChild = GF.IncludeParent | GF.IncludeChildren,

        ParnetOrAllChildren = GF.IncludeParent | GF.OrParent | GF.IncludeChildren,
        ParnetAndAllChildren = GF.IncludeParent | GF.IncludeChildren
    }

   

    public abstract class NestedGenerator : Generator
    {
        [Header("NestedGenerator Settings")]
        public Generator Parent;
        public List<Generator> ChildGenerators;
        public ShouldGenerateOptions Options;

        private void Awake()
        {
            foreach (Generator gen in ChildGenerators)
            {
                OnStartGenerating += gen.StartGenerating;
                OnStopGenerating += gen.StopGenerating;
            }
        }

        public override void Generate()
        {
            Parent.Generate();

            foreach (Generator gen in ChildGenerators)
            {
                gen.Generate();
            }
        }

        public override bool ShouldGenerate()
        {
            bool pResult = false;

            if (Options.HasFlag(GF.IncludeParent))
            {
                pResult = Parent.ShouldGenerate();
            }
            else
            {
                pResult = !Options.HasFlag(GF.OrChildren); 
            }

            if (!Options.HasFlag(GF.IncludeChildren))
            {
                return pResult; 
            }

            
            //include children bussniess 
            bool earlyTermination;

            Func<bool, bool, bool> operation = (earlyTermination = Options.HasFlag(GF.OrChildren))?  operation = (a, b) => a | b : operation = (a, b) => a & b;

            bool cResult = !earlyTermination; 
            foreach (Generator g in ChildGenerators)
            {
                bool should = g.ShouldGenerate();

                cResult = operation(cResult, should); 

                if(should == earlyTermination)
                {
                    break; 
                }
            }

            operation = Options.HasFlag(GF.OrParent)?  (a, b) => a | b : operation = (a, b) => a & b;

            pResult = operation(pResult, cResult); 

            
            return pResult;
        }

        public override void SkippedGeneration()
        {
            Parent.Generate();

            foreach (Generator gen in ChildGenerators)
            {
                gen.SkippedGeneration();
            }
        }
    }

}