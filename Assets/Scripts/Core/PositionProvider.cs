using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core
{
    public enum Direction
    {
        Transform = -1,
        Center = 0x00,
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

    /// <summary>
    /// Provides Positions relative to camera sides.
    /// </summary>
    [Serializable]
    public class PositionProvider
    {
        [SerializeField]
        private Direction Direction;

        [SerializeField]
        private Vector2 Offset;

        [SerializeField]
        private PositionRelativeTo RelativeTo;
        
        [SerializeField]
        private Transform Refrence;

        public Vector3 ProvidedPosition
        {
            get
            {
                if (Direction == Direction.Transform)
                {
                    if(Refrence == null)
                    {
                        throw new NullReferenceException("Transform must be set"); 
                    }
                    return Refrence.position + (Vector3)Offset; 
                }

                return GetScreenPosition(Direction, Offset, RelativeTo); 
            }
        }

        public Vector3 OppositePosition
        {
            get {
                if (Direction == Direction.Transform)
                {
                    if (Refrence == null)
                    {
                        throw new NullReferenceException("Transform must be set");
                    }
                    return Refrence.position - (Vector3)Offset;
                }

                return GetScreenPosition(Direction, Offset, (PositionRelativeTo)(-1 * (int)RelativeTo));
            }
        }

        public void SetToTransform(Transform transform)
        {
            SetToTransform(transform, Vector2.zero); 
        }

        public void SetToTransform(Transform transform, Vector2 offset)
        {
            Direction = Direction.Transform;
            Offset = offset; 
            RelativeTo = PositionRelativeTo.None;
            Refrence = transform;
        }

        public void SetToPosition(Direction d, Vector2 offset, PositionRelativeTo r = PositionRelativeTo.None)
        {
            Direction = d;
            Offset = offset;
            RelativeTo = r;
            Refrence = null;
        }

        public void SetToPosition(Direction d)
        {
            SetToPosition(d, Vector2.zero); 
        }

        #region Static Helper Methods
        public static Vector2 GetScreenPosition(Direction dir, Vector2 offset, PositionRelativeTo m)
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

        public static Vector2 GetScreenPosition(Direction dir, Vector2 offset)
        {
            return GetScreenPosition(dir, offset, PositionRelativeTo.None);
        }

        public static Vector2 GetScreenPosition(Direction dir)
        {
            return GetScreenPosition(dir, Vector2.zero, PositionRelativeTo.None);
        }

        public static float GetScreenWidth()
        {
            return GetScreenHeight() * Camera.main.aspect;
        }

        public static float GetScreenHeight()
        {
            return 2 * Camera.main.orthographicSize;
        } 
        #endregion
    }
}
