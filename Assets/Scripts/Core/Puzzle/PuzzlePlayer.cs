using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Puzzle
{
    public class PuzzlePlayer : TileContent
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
                Move(NeighborDirection.North);
            else if (Input.GetKeyDown(KeyCode.D))
                Move(NeighborDirection.East);
            else if (Input.GetKeyDown(KeyCode.A))
                Move(NeighborDirection.West);
            else if (Input.GetKeyDown(KeyCode.S))
                Move(NeighborDirection.South);
        }
    }

}