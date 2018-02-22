using System;


namespace ChainedRam.Core.Generation
{
    [Flags]
    public enum TerminationType
    {
        Internal = 0,
        External = 1,
        Duration = 2,
        Counter = 3,
        OnSkip = 4
    }
}

