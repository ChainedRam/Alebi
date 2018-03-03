using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedGeneration : MonoBehaviour
{
    public abstract float Seconds { get; }

    public abstract void Trigger(Generator sender); 
}
