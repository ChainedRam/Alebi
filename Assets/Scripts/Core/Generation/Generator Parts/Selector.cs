using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Compnenet that implements a select method to select an element from a list. 
/// </summary>
public abstract class Selector : MonoBehaviour
{
    public abstract T Select<T>(T[] list, T prev = null) where T : class;
}
