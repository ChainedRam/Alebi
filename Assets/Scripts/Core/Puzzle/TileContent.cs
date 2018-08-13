using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Puzzle
{
    public abstract class TileContent : MonoBehaviour
    {
        public float speed;
        public bool isMoving
        {
            private set; get;
        }

        private void Start()
        {
            speed = 0.075f;
            isMoving = true;
        }
        public Tile parent
        {
            get
            {
                return transform.parent.GetComponent<Tile>();
            }
        }

        public virtual bool Move(NeighborDirection dire)
        {
            if (isMoving)
            {
                return false;
            }
            if (parent.HasNeighbor(dire) == false || parent.GetNeighbor(dire).HasContent())
            {
                return false;
            }
            parent.GetNeighbor(dire).SetContent(this);
            return true;
        }

        private void LateUpdate()
        {
            bool isXMoving = false;
            bool isYMoving = false;
            float offsetX;
            float offsetY;
            if (Mathf.Abs(transform.localPosition.x) >= speed)
            {
                isXMoving = true;
                offsetX = speed * Mathf.Sign(transform.localPosition.x);
            }
            else
            {
                offsetX = transform.localPosition.x;
            }
            if (Mathf.Abs(transform.localPosition.y) >= speed)
            {
                isYMoving = true;
                offsetY = speed * Mathf.Sign(transform.localPosition.y);
            }
            else
            {
                offsetY = transform.localPosition.y;
            }
            transform.localPosition -= new Vector3(offsetX, offsetY, 0);
            isMoving = (isXMoving | isYMoving);
        }
    }
}