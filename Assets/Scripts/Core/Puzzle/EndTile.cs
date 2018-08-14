using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Puzzle
{
    public class EndTile : Tile
    {
        public override bool SetContent(TileContent content)
        {
            if (content is PuzzlePlayer)
            {
                return  base.SetContent(content);
            }
            else
                return false;
        }
    }
}
