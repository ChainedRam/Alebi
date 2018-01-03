using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    /// <summary>
    /// Wraps a generator around it. Can be used to refrence other generator. 
    /// </summary>
    public class GeneratorWrap : Generator
    {
        #region Inspector Attribute 
        [Header("GeneratorWrap")]
        public Generator Wrapped;
        #endregion
        #region Unity Methods 

        protected override void WhenAwake()
        {
            base.WhenAwake();
            Attach(Wrapped);
        }
        #endregion
        #region Generator Overrides 

        protected override bool ShouldGenerate()
        {
            return Wrapped.IsGenerating;
        }
        #endregion
    }
}
