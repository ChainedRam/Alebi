using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Exceptions
{
    public class GeneratorBeginException : Exception
    {
        public GeneratorBeginException(string message) : base(message)
        {
        }
    }
}
