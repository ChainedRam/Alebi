using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGeneratorTerminatorDeleteMe
{
    void Setup(Generator generator);
    bool ShouldTerminate(Generator generator);
    void SetApart(Generator generator);
}
