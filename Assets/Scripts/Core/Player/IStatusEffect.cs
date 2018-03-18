using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Player
{
    public interface IStatusEffect
    {
        string Name { get; }

        void Init(Player p); 
        void Apply(Player p);
        void Revert(Player p);
    }
}
