using ChainedRam.Core.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    /// <summary>
    /// A generator that delegates it's abstract functionalities into separate components. 
    /// </summary>
    public class ComponentGenerator : Generator
    {
        #region Inspecter Attributes 
        public GenerateComponent GenerateComponent; 
        #endregion
        #region Custom Inspector Attributes
        [HideInInspector()]
        public bool PrintDebug;

        [HideInInspector()]
        public TerminationType TerminatorType = TerminationType.Internal;

        [HideInInspector()]
        public GenerationType GenerateConditionType = GenerationType.Internal;

        [HideInInspector()]
        public GeneratorTerminatorComponent Terminator;

        [HideInInspector()]
        public GeneratorConditionComponent GenerateCondition;
        #endregion
        #region Overrride
        protected override void OnBegin()
        {
            base.OnBegin();
            GenerateCondition?.Setup(this);
            Terminator?.Setup(this);
        }

        protected override void OnEnd()
        {
            base.OnEnd();
            GenerateCondition?.SetApart(this);
            Terminator?.SetApart(this);
        }

        protected override void Awake()
        {
            base.Awake();
            Terminator?.Setup(this);
            GenerateCondition?.Setup(this);
        }

        protected override void OnGenerate(GenerateEventArgs e)
        {
            GenerateComponent.Generate(e);
        }

        protected override bool ShouldTerminate()
        {
            return Terminator?.ShouldTerminate(this) ?? false;
        }

        protected override bool ShouldGenerate()
        {
            return GenerateCondition?.ShouldGenerate(this) ?? true; 
        } 
        #endregion
    }
}

