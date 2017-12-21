using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains flags in which a <see cref="DialogBox" will pause upon reaching such charahcters. />
/// </summary>
public enum DialogPauseProperty : int
{
    None = 0,
    PageEnd = 1,
    NewLine = 2,
    Space = 4,
    NewPage = 8,
}
