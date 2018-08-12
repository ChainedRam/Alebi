using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Puzzle
{
    public abstract class TileContent : MonoBehaviour
    {
        public Tile parent;

        public bool Move(NeighborDirection dire)
        {
            if (parent.HasNeighbor(dire) == false || parent.GetNeighbor(dire).HasContent())
            {
                return false;
            }
            parent.GetNeighbor(dire).SetContent(this);
            this.transform.localPosition = new Vector2(0, 0);
            return true;
        }
    }

}