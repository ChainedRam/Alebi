using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Puzzle
{
    public class PuzzlePlayer : TileContent
    {
        void Update()
        {
            if (isMoving)
                return;
            if (Input.GetKeyDown(KeyCode.W))
                Move(NeighborDirection.North);
            else if (Input.GetKeyDown(KeyCode.D))
                Move(NeighborDirection.East);
            else if (Input.GetKeyDown(KeyCode.A))
                Move(NeighborDirection.West);
            else if (Input.GetKeyDown(KeyCode.S))
                Move(NeighborDirection.South);
        }
        public override bool Move(NeighborDirection dire)
        {
            if (base.Move(dire))
                return true;
            if (!parent.HasNeighbor(dire))
                return false;
            if (parent.GetNeighbor(dire).content.Move(dire))
                return base.Move(dire);
            else
                return false;
        }
    }

}