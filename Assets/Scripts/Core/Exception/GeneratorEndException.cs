using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Exceptions
{
    public class GeneratorEndException : Exception
    {
        public GeneratorEndException(string message) : base(message)
        {
        }
    }
}
