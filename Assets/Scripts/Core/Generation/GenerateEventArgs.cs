using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    public class GenerateEventArgs : EventArgs
    {
        public float Delta;

        public GenerateEventArgs(float delta)
        {
            Delta = delta;
        }
    }
}