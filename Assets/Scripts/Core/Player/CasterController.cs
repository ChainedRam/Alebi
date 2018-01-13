using ChainedRam.Core.Collection;
using ChainedRam.Core.Enums;
using ChainedRam.Core.Generation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///TODO: lots of shit
/// </summary>
public abstract class CasterController<A> : MonoBehaviour
{

    //TODO
    public abstract IEnumerable<Caster<A>> CasterListeners { get; }
    
    public abstract Dictionary<KeyCode, A> KeyDownMap { get; protected set; }

    /// <summary>s
    /// protecteded
    /// </summary>
    protected void OnGUI()
    {
        Event current = Event.current; 
        switch (current.type)
        {
            case EventType.KeyDown:
                if (KeyDownMap.ContainsKey(current.keyCode))
                {
                    print("Cast " + KeyDownMap[current.keyCode]); 
                   //RequestCasters(KeyDownMap[current.keyCode]); 
                }
                break; 
        }
    }

    private void RequestCasters(A action)
    {
        foreach(var caster in CasterListeners)
        {
            caster.RequestCast(action); 
        }
    }
}
public class CasterController : CasterController<string>
{
    public KeyCodeString[] KeyCodeString;

    private void Start()
    {
        KeyDownMap = KeyCodeString.ToDictionary(p => (KeyCode) p.Key.Value, p => p.Value);
    }

    public override IEnumerable<Caster<string>> CasterListeners
    {
        get
        {
            return null; 
        }
    }

    public override Dictionary<KeyCode, string> KeyDownMap
    {
        get;
        protected set;
    }
}


[Serializable]
public class KeyCodeString : EnumString<KeyCode>
{
}

