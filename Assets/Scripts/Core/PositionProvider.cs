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
        Outside = -1, 
    }

    public enum RotationFacing
    {
        #region Single Absolute Directions
        /// <summary>
        /// 
        /// </summary>
        Default = 0x00,

        /// <summary>
        /// Points to absolote North
        /// </summary>
        North = 0x01,

        /// <summary>
        /// Points to absolote South
        /// </summary>
        South = 0x02,

        /// <summary>
        /// Points to absolote East
        /// </summary>
        East = 0x04,

        /// <summary>
        /// Points to absolote West
        /// </summary>
        West = 0x08,
        #endregion
        #region Combination Absolute Directions
        /// <summary>
        /// Points to absolote NorthEast
        /// </summary>
        NorthEast = North | East,

        /// <summary>
        /// Points to absolote NorthWest
        /// </summary>
        NorthWest = North | West,

        /// <summary>
        /// Points to absolote SouthEast
        /// </summary>
        SouthEast = South | East,

        /// <summary>
        /// Points to absolote SouthWest
        /// </summary>
        SouthWest = South | West,
        #endregion
        #region Special Directions
        /// <summary>
        /// Points to a rotation based on a given agnle (in degrees). 
        /// </summary>
        Numaric = 0xF0,
        
        /// <summary>
        /// Points to scene center (inside) or the opposite (outside) base on <see cref="PositionRelativeTo"/>.
        /// </summary>
        Relative = 0xF1,

        /// <summary>
        /// Copies angle based on given transform. 
        /// </summary>
        MatchTransform = 0xF2,

        /// <summary>
        /// Tilt rotation to face given transform. 
        /// </summary>
        FaceTransform = 0xF3,
        #endregion
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
        private float Degree;

        [SerializeField]
        private PositionRelativeTo RelativeTo;

        [SerializeField]
        private RotationFacing RotationFacing;

        [SerializeField]
        private Transform PositionRefrence;

        [SerializeField]
        private Transform RotationRefrence;


        public Vector3 ProvidedPosition
        {
            get
            {
                if (Direction == Direction.Transform)
                {
                    if(PositionRefrence == null)
                    {
                        throw new NullReferenceException("Transform must be set"); 
                    }
                    return PositionRefrence.position + (Vector3)Offset; 
                }

                return GetScreenPosition(Direction, Offset, RelativeTo); 
            }
        }

        public Vector3 OppositePosition
        {
            get {
                if (Direction == Direction.Transform)
                {
                    if (PositionRefrence == null)
                    {
                        throw new NullReferenceException("Transform must be set");
                    }
                    return PositionRefrence.position - (Vector3)Offset;
                }

                return GetScreenPosition(Direction, Offset, (PositionRelativeTo)(-1 * (int)RelativeTo));
            }
        }

        public float? ProvidedRotation
        {
            get
            {
                switch (RotationFacing)
                {
                    case RotationFacing.Default:
                        return null; 
                    case RotationFacing.North:
                        return 0;
                    case RotationFacing.NorthEast:
                        return 315;
                    case RotationFacing.East:
                        return 270;
                    case RotationFacing.SouthEast:
                        return 225;
                    case RotationFacing.South:
                        return 180;
                    case RotationFacing.SouthWest:
                        return 135;
                    case RotationFacing.West:
                        return 90;
                    case RotationFacing.NorthWest:
                        return 45;
                    case RotationFacing.Numaric:
                        return Degree;
                    case RotationFacing.Relative:
                        switch (RelativeTo)
                        {
                            case PositionRelativeTo.Inside:
                                return AngleBetween(Vector2.zero, ProvidedPosition);                                 
                            case PositionRelativeTo.Outside:
                                return 180 + AngleBetween(Vector2.zero, ProvidedPosition);
                            default:
                                return null; 
                        }
                    case RotationFacing.MatchTransform:
                        return RotationRefrence.transform.eulerAngles.z;
                    case RotationFacing.FaceTransform:
                        return AngleBetween(RotationRefrence.position, ProvidedPosition);

                }

                //should never reach here
                return null; 
            }
        }

        /// Because Fuck Vector2.Angle & it's sister Vector2.SignedAngle. 
        private float AngleBetween(Vector2 from, Vector2 to)
        {
            return Mathf.Atan2(from.y - to.y, from.x - to.x) * Mathf.Rad2Deg - 90;
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
            PositionRefrence = transform;
        }

        public void SetToPosition(Direction d, Vector2 offset, PositionRelativeTo r = PositionRelativeTo.None)
        {
            Direction = d;
            Offset = offset;
            RelativeTo = r;
            PositionRefrence = null;
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
