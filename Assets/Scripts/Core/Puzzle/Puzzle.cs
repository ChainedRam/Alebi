using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainedRam.Core.Dialog;

namespace ChainedRam.Core.Puzzle
{
    public class Puzzle : MonoBehaviour
    {
        public Tile[][] tile;

        public Puzzle nextPuzzle;
        public DialogBox dialogBox;
        public Dialog.Dialog startDialogue;

        private void Start()
        {
            SetState();
        }
        private void OnEnable()
        {
            if (dialogBox != null)
            {
                Pause();
                startDialogue.OnEnd += Resume;
                dialogBox.StartDialog(startDialogue);
            }
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
        public void Pause()
        {
            GetComponentInChildren<PuzzlePlayer>().enabled = false;
            print("for i have paused");
        }
        public void Resume()
        {
            GetComponentInChildren<PuzzlePlayer>().enabled = true;
            print("for i have resumed");
        }
    }


}