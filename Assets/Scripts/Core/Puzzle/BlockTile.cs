using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Puzzle
{
    public class BlockTile : Tile
    {
        public override bool SetContent(TileContent content)
        {
            return false;
        }
    }
}
