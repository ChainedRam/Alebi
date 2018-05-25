using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChainedRam.Core
{
    [Flags]
    public enum Direction
    {
        Center = 0x00,

        North = 1 << 0,
        South = 1 << 1,
        East  = 1 << 2,
        West  = 1 << 3,

        NorthEast = North | East,
        NorthWest = North | West,
        SouthEast = South | East,
        SouthWest = South | West,
    }

    public enum PositionLocation
    {
        Transform,
        Direction,
        Random, 
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
        /// Tilt rotation to face given transform. 
        /// </summary>
        Inside = 0xF2,

        /// <summary>
        /// Tilt rotation to face given transform. 
        /// </summary>
        Outside = 0xF3,

        /// <summary>
        /// Copies angle based on given transform. 
        /// </summary>
        MatchTransform = 0xF4,

        /// <summary>
        /// Tilt rotation to face given transform. 
        /// </summary>
        FaceTransform = 0xF5,
        #endregion
    }


    [Flags]
    public enum RandomPositionOption
    {
        Transforms  = 1 << 0,
        Center      = 1 << 1,
        North       = 1 << 2,
        South       = 1 << 3,
        East        = 1 << 4,
        West        = 1 << 5,
        NorthEast   = 1 << 6,
        NorthWest   = 1 << 7,
        SouthEast   = 1 << 8,
        SouthWest   = 1 << 9,
    }

    public enum RotationOffsetType
    {
        Numaric, 
        Vector, 
        Random,
    }

    /// <summary>
    /// Provides Positions relative to camera sides.
    /// </summary>
    [Serializable]
    public class PositionProvider
    {
        [SerializeField]
        private PositionLocation Location;

        [SerializeField]
        private Direction Direction;

        [SerializeField]
        private Vector2 PositionOffset;

        [SerializeField]
        private Vector2 RandomMultitude;

        [SerializeField]
        private RandomPositionOption RandomPositionOption;

        [SerializeField]
        private Transform[] RandomTransforms;

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

        [SerializeField]
        private RotationOffsetType RotationOffsetType;

        [SerializeField]
        private float RotationOffset;

        [SerializeField]
        private Vector2 RotationOffsetVector;

        [SerializeField]
        private float RotationRandomOffset;

        private Vector3? LastProvidedPosition;

        private List<int> RandomTransformMemory;

        private List<int> RandomMemory; 

        public Vector3 ProvidedPosition
        {
            get
            {
                return (LastProvidedPosition = CalulatePosition()).Value;
            }
        }

        public Vector3 OppositePosition
        {
            get {
                if (Location == PositionLocation.Transform)
                {
                    if (PositionRefrence == null)
                    {
                        throw new NullReferenceException("Transform must be set");
                    }
                    return PositionRefrence.position - (Vector3)PositionOffset;
                }

                return GetScreenPosition(Direction, PositionOffset, (PositionRelativeTo)(-1 * (int)RelativeTo));
            }
        }

        public float? ProvidedRotation
        {
            get
            {
                float? calc = CalculateRotation();


                if (calc.HasValue)
                {
                    float RandomDegree = 0; 
                    switch (RotationOffsetType)
                    {
                        //RotationOffsetType.Vector happends within CalculateRotation(); 
                        case RotationOffsetType.Random:
                            RandomDegree = UnityEngine.Random.Range(0, RotationRandomOffset); 
                            goto case RotationOffsetType.Numaric; 
                        case RotationOffsetType.Numaric:
                            return calc + RotationOffset + RandomDegree;
                    }
                }

                return calc;
            }
        }

        private float? CalculateRotation()
        {
            LastProvidedPosition = LastProvidedPosition ?? ProvidedPosition;

            Vector2 OffsettedPrevPosition = (Vector2)LastProvidedPosition.Value + (RotationOffsetType == RotationOffsetType.Vector?  RotationOffsetVector : Vector2.zero);
            //thing about how this works?? how offset can join it 

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
                            return AngleBetween(Vector2.zero, OffsettedPrevPosition);
                        case PositionRelativeTo.Outside:
                            return 180 + AngleBetween(Vector2.zero, OffsettedPrevPosition);
                        default:
                            return null;
                    }
                case RotationFacing.Inside:
                    return AngleBetween(Vector2.zero, OffsettedPrevPosition);
                case RotationFacing.Outside:
                    return 180 + AngleBetween(Vector2.zero, OffsettedPrevPosition);
                case RotationFacing.MatchTransform:
                    if (RotationRefrence == null)
                    {
                        return null;
                    }
                    return RotationRefrence.transform.eulerAngles.z;
                case RotationFacing.FaceTransform:
                    if (RotationRefrence == null)
                    {
                        return null;
                    }
                    return AngleBetween(RotationRefrence.position, OffsettedPrevPosition);
            }

            //should never reach here
            return null;
        }

        public void SetToTransform(Transform transform)
        {
            SetToTransform(transform, Vector2.zero); 
        }

        public void SetToTransform(Transform transform, Vector2 offset)
        {
            Location = PositionLocation.Transform;
            PositionOffset = offset; 
            RelativeTo = PositionRelativeTo.None;
            PositionRefrence = transform;
        }

        public void SetToPosition(Direction d, Vector2 offset, PositionRelativeTo r = PositionRelativeTo.None)
        {
            Location = PositionLocation.Direction; 
            Direction = d;
            PositionOffset = offset;
            RelativeTo = r;
            PositionRefrence = null;
        }

        public void SetToPosition(Direction d)
        {
            SetToPosition(d, Vector2.zero); 
        }

        public void SetRotationOffset(float degree)
        {
            RotationOffsetType = RotationOffsetType.Numaric;
            RotationOffset = degree; 
        }

        public void SetRotationOffset(Vector2 rotationOffset)
        {
            RotationOffsetType = RotationOffsetType.Vector;
            RotationOffsetVector = rotationOffset;
        }

        public void SetRotationFacing(Transform t)
        {
            RotationFacing = RotationFacing.FaceTransform;
            RotationRefrence = t; 
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

        /// Because Fuck Vector2.Angle & it's sister Vector2.SignedAngle. 
        public static float AngleBetween(Vector2 from, Vector2 to)
        {
            return Mathf.Atan2(from.y - to.y, from.x - to.x) * Mathf.Rad2Deg - 90;
        }
        #endregion

        public PositionProvider Copy()
        {
            PositionProvider p = new PositionProvider()
            {
                Location = Location,
                Direction = Direction,
                PositionOffset = PositionOffset,
                RandomMultitude = RandomMultitude,
                RandomPositionOption = RandomPositionOption,
                RandomTransforms = RandomTransforms.ToArray(),
                Degree = Degree,
                RelativeTo = RelativeTo,
                RotationFacing = RotationFacing,
                PositionRefrence = PositionRefrence,
                RotationRefrence = RotationRefrence,
                RotationOffset = RotationOffset,
                RotationOffsetType = RotationOffsetType,
                RotationOffsetVector = RotationOffsetVector,
                RotationRandomOffset = RotationRandomOffset,
                RandomMemory = RandomMemory?.ToList(),
                RandomTransformMemory = RandomTransformMemory?.ToList(),
                LastProvidedPosition = LastProvidedPosition,
            };

            return p; 
        }

        public void CopyValues(PositionProvider source)
        {
            Location = source.Location;
            Direction = source.Direction;
            PositionOffset = source.PositionOffset;
            RandomMultitude = source.RandomMultitude;
            RandomPositionOption = source.RandomPositionOption;
            RandomTransforms = source.RandomTransforms.ToArray();
            Degree = source.Degree;
            RelativeTo = source.RelativeTo;
            RotationFacing = source.RotationFacing;
            PositionRefrence = source.PositionRefrence;
            RotationRefrence = source.RotationRefrence;
            RotationOffset = source.RotationOffset;
            RotationOffsetType = source.RotationOffsetType;
            RotationOffsetVector = source.RotationOffsetVector;
            RotationRandomOffset = source.RotationRandomOffset; 
        }

        private Vector3 CalulatePosition()
        {
            switch (Location)
            {
                case PositionLocation.Transform:
                    if (PositionRefrence == null)
                    {
                        throw new NullReferenceException("Transform must be set");
                    }
                    return PositionRefrence.position + (Vector3)PositionOffset;

                case PositionLocation.Direction:
                    return GetScreenPosition(Direction, PositionOffset, RelativeTo);

                case PositionLocation.Random:
                    var matching = Enum.GetValues(typeof(RandomPositionOption))
                       .Cast<RandomPositionOption>()
                       .Where(c => (RandomPositionOption & c) == c)
                       .ToArray();

                    if (matching.Length == 0)
                    {
                        throw new Exception("Random cannot operate without options.");
                    }

                    if (RandomMemory == null)
                    {
                        RandomMemory = new List<int>();
                    }
                    else if (RandomMemory.Count() >= matching.Length)
                    {
                        RandomMemory.Clear(); 
                    }

                    System.Random rand = new System.Random();
                    int selected;

                    do
                    {
                        selected = rand.Next(matching.Length);
                    } while (matching.Length > 1 && RandomMemory.Contains(selected));

                    RandomMemory.Add(selected);

                    RandomPositionOption myEnum = matching[selected];

                    Direction randomDirection;

                    Vector2 randomOffset = Vector2.Scale(
                        RandomMultitude,
                        new Vector2(UnityEngine.Random.value, UnityEngine.Random.value)
                        );

                    switch (myEnum)
                    {
                        case RandomPositionOption.Transforms:

                            if (RandomTransformMemory == null)
                            {
                                RandomMemory = new List<int>();
                            }
                            else if (RandomTransformMemory.Count() >= matching.Length)
                            {
                                RandomMemory.Clear();
                            }
                          
                            do
                            {
                                selected = rand.Next(RandomTransforms.Length);
                            } while (RandomTransforms.Length > 1 && RandomTransformMemory.Contains(selected));

                            RandomTransformMemory.Add(selected); 

                            if (RandomTransforms.Length == 0)
                            {
                                throw new Exception("Empty array of transforms.");
                            }

                            if (RandomTransforms[selected] == null)
                            {
                                throw new NullReferenceException("Transform at " + selected + " is null.");
                            }

                            return RandomTransforms[selected].position + (Vector3)(randomOffset + PositionOffset);
                        case RandomPositionOption.Center:
                            randomDirection = Direction.Center;
                            break;
                        case RandomPositionOption.East:
                            randomDirection = Direction.East;
                            break;
                        case RandomPositionOption.West:
                            randomDirection = Direction.West;
                            break;
                        case RandomPositionOption.South:
                            randomDirection = Direction.South;
                            break;
                        case RandomPositionOption.North:
                            randomDirection = Direction.North;
                            break;
                        case RandomPositionOption.NorthEast:
                            randomDirection = Direction.NorthEast;
                            break;
                        case RandomPositionOption.NorthWest:
                            randomDirection = Direction.NorthWest;
                            break;
                        case RandomPositionOption.SouthEast:
                            randomDirection = Direction.SouthEast;
                            break;
                        case RandomPositionOption.SouthWest:
                            randomDirection = Direction.SouthWest;
                            break;
                        default:
                            throw new Exception("Missing Random Options");
                    }

                    return GetScreenPosition(randomDirection, PositionOffset + randomOffset, RelativeTo);
                default:
                    break;
            }

            throw new Exception("What the fuck?");
        }

        public void ResetRNG()
        {
            RandomMemory = null;
            RandomTransformMemory = null; 
        }
    }
}
