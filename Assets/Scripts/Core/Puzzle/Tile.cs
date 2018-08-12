using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Puzzle
{
    public class Tile : MonoBehaviour, NeighbotGrid<Tile>.INeighbor<Tile>
    {
        public TileContent content;
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
            Neighbor = new Tile[4];
        }

        public bool IsEmpty()
        {
            return content == null;
        }

        public void SetContent(TileContent content)
        {
            if(content.parent != null)
                content.parent.EmptyContent();
            content.parent = this;
            this.content = content;

            content.transform.SetParent(this.transform);
        }
        private void EmptyContent()
        {
            if (HasContent())
            {
                content.parent = null;
            }
            content = null;
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