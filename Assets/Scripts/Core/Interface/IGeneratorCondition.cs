using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGeneratorConditionDeleteMe
{
    void Setup(Generator generator);
    bool ShouldGenerate(Generator generator);
    void SetApart(Generator generator);
}
