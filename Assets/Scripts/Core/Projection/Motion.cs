using ChainedRam.Core.Extentions;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Motion : MonoBehaviour
{
    public abstract Vector2 GetOffset();

    public virtual Vector2 Apply(Vector2 vector) { return GetOffset() + vector; }

    public virtual void Initialize(){}
}