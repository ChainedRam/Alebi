using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Puzzle
{
    public class Puzzle : MonoBehaviour
    {
        public Tile[][] tile;

        private void Start()
        {
            SetState();
        }

        [ContextMenu("SetState")]
        public void SetState()
        {
            TileContent[] children = GetComponentsInChildren<TileContent>();
            foreach (var item in children)
            {
                item.originalParent = item.parent;
            }
        }

        [ContextMenu("ResetPuzzle")]
        public void ResetPuzzle()
        {
            TileContent[] children = GetComponentsInChildren<TileContent>();
            foreach (var item in children)
            {
                item.resetToOriginal();
            }
        }
    }


}