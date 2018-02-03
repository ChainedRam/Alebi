using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    /// <summary>
    /// Generates wrapped generator once. ONCE! 
    /// </summary>
    public class OnceGenerator : GeneratorWrap
    {
        #region Private Atributes 
        private bool HasGenerated;
        #endregion
        #region Unity Methods 
        #endregion
        #region GeneratorWrap Override 
        protected sealed override bool ShouldGenerate()
        {
            return !HasGenerated;
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            HasGenerated = false;
            OnGenerateEventHandler += (s,e) => HasGenerated = true; //REEEEEEE. move to WhenGenerate
        }
        #endregion
    }
}
