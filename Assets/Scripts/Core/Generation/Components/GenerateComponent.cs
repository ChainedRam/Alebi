using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenerateComponent : MonoBehaviour
{
    public abstract void Generate(GenerateEventArgs e); 
}
