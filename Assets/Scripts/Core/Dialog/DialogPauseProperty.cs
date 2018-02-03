using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Dialog
{
    /// <summary>
    /// Contains flags in which a <see cref="DialogBox" will pause upon reaching such charahcters. />
    /// </summary>
    public enum DialogPauseType : int
    {
        None = 0,
        End = 1,
        NewLine = 2,
        Space = 4,
        NewPage = 8,
    }
}
