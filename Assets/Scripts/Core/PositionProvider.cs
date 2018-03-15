using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core
{
    public enum Direction
    {
        None = 0x00,
        //Single
        North = 0x01,
        South = 0x02,
        East = 0x04,
        West = 0x08,
        //Combo
        NorthEast = North | East,
        NorthWest = North | West,
        SouthEast = South | East,
        SouthWest = South | West,
    }

    public enum PositionRelativeTo
    {
        Inside = 1, 
        None = 0, 
        OutSide = -1, 
    }


    public class PositionProvider : MonoBehaviour
    {
        public Vector2 GetScreenPosition(Direction dir, Vector2 offset, PositionRelativeTo m)
        {
            int mov = (int)m;

            float width = offset.x;
            float height = offset.y;

            if (dir.HasFlag(Direction.North))
            {
                height = GetScreenHeight() / 2 - (mov * offset.y);
            }
            else if (dir.HasFlag(Direction.South))
            {
                height = -GetScreenHeight() / 2 + (mov * offset.y);
            }

            if (dir.HasFlag(Direction.East))
            {
                width = GetScreenWidth() / 2 - (mov * offset.x);
            }
            else if (dir.HasFlag(Direction.West))
            {
                width = -GetScreenWidth() / 2 + (mov * offset.x);
            }

            return new Vector2(width, height);
        }

        public Vector2 GetScreenPosition(Direction dir, Vector2 offset)
        {
            return GetScreenPosition(dir, offset, PositionRelativeTo.None); 
        }

        public Vector2 GetScreenPosition(Direction dir)
        {
            return GetScreenPosition(dir, Vector2.zero, PositionRelativeTo.None);
        }

        public float GetScreenWidth()
        {
            return GetScreenHeight() * Camera.main.aspect;
        }

        public float GetScreenHeight()
        {
            return 2 * Camera.main.orthographicSize;
        }
    }
}
