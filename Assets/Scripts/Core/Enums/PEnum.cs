
using System;
using UnityEngine;

namespace ChainedRam.Core.Enums
{
    public enum Default{ NotSet }

    /// <summary>
    /// An enum wrap that can be rendered with <see cref="Collection.KeyValue{K, V}"/>
    /// </summary>
    [Serializable]
    public class PEnum
    {
        public virtual Enum Value
        {
            set
            {
                EnumValue = Convert.ToInt32(value);
            }
            get
            {
                return (Enum)Enum.ToObject(EnumType, EnumValue);
            }
        }

        [SerializeField]
        public int EnumValue;

        [SerializeField]
        public Type EnumType;

        public PEnum(Type type)
        {
            EnumType = type; 
            EnumValue = 0; 
        }

        public PEnum(Enum e) : this(e.GetType())
        {  
            EnumValue = Convert.ToInt32(e);
        }

        public static implicit operator Enum(PEnum e)
        {
            return e.Value;
        }

        public static implicit operator PEnum(Enum e)
        {
            return new PEnum(e);
        }
    }

    public class PEnum<T> : PEnum
    {
        public PEnum() : base(typeof(T))
        { 
            EnumValue = 0; 
        }

        public T GetValue()
        {
            return (T)Enum.ToObject(EnumType, EnumValue); ;
        }
    }
}

