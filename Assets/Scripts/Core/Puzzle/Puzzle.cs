using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Puzzle
{
    public class Puzzle : MonoBehaviour
    {
        public Tile[][] tile;

        public Puzzle nextPuzzle;

        private void Start()
        {
            SetState();
        }

        public void End()
        {
            this.gameObject.SetActive(false);
            print("puzzle ended");
            if (nextPuzzle != null)
            {
                nextPuzzle.gameObject.SetActive(true);
            }
            else
            {
                print("game finished");
            }
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