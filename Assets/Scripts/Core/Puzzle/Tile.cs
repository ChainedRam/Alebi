using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Puzzle
{
    public class Tile : MonoBehaviour, NeighbotGrid<Tile>.INeighbor<Tile>
    {
        public TileContent content
        {
            get
            {
                //if (transform.GetChild(0) != null)
                    return transform.GetComponentInChildren<TileContent>();
            }
        }
        [SerializeField]
        public Tile[] neighbor;
        public Tile[] Neighbor
        {
            set
            {
                this.neighbor = value;
            }
            get
            {
                return this.neighbor;
            }
        }
        private void Awake()
        {
            if(Neighbor == null)
                Neighbor = new Tile[4];
        }

        public bool IsEmpty()
        {
            return content == null;
        }

        public virtual bool SetContent(TileContent content)
        {
            content.transform.SetParent(this.transform);
            return true;
        }
        public bool HasContent()
        {
            return content != null;
        }

        public bool HasNeighbor(NeighborDirection direction)
        {
            return neighbor[(int)direction] != null;
        }

        public Tile GetNeighbor(NeighborDirection direction)
        {
            return neighbor[(int)direction];
        }
    }

}