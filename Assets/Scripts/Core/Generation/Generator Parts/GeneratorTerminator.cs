using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GeneratorTerminator : MonoBehaviour
{
    public virtual void Setup(Generator gen) { }
    public virtual void SetApart(Generator gen) { }

    public abstract bool ShouldTerminate(Generator gen);
}
