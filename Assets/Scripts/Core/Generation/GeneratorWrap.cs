using ChainedRam.Core.Generation.Extention;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    /// <summary>
    /// Wraps a generator around it. Can be used to refrence other generator. 
    /// </summary>
    public class GeneratorWrap : ComponentGenerator
    {
        #region Inspector Attribute 
        [Header("GeneratorWrap")]
        public ComponentGenerator Wrapped;
        #endregion
        #region Unity Methods 

        protected override void Awake()
        {
            base.Awake();
            this.Attach(Wrapped);
        }
        #endregion
        #region Generator Overrides 

        protected override bool ShouldGenerate()
        {
            return Wrapped.enabled;
        }

        protected override bool ShouldTerminate()
        {
            return Wrapped.enabled == false;
        }
        #endregion
    }
}
