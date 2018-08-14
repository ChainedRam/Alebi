using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Puzzle
{
    [Obsolete]
    public class BlockContent : TileContent
    {
        public override bool Move(NeighborDirection dire)
        {
            return false;
        }
    }
}
