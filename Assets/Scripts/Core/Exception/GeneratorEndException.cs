using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorEndException : Exception
{
    public GeneratorEndException(string message) : base(message)
    {
    }
}
