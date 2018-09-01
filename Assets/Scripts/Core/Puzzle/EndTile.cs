using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ChainedRam.Core.Puzzle
{
    public class EndTile : Tile
    {
        public UnityEvent onPlayerReached;
        public override void OnContentReached()
        {
            onPlayerReached.Invoke();
        }
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
