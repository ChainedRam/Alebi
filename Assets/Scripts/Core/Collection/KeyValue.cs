using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using ChainedRam.Core.Enums;


namespace ChainedRam.Core.Collection
{
    [Serializable]
    public class KeyValue<K, V>
    {
        public K Key;

        public V Value;

        public KeyValue()
        {
            Key = default(K);
            Value = default(V);
        }

        public KeyValue(K key, V value)
        {
            Key = key;
            Value = value;
        }
    }
    #region Enum Enum

    public class EnumValue<E, V> : KeyValue<PEnum, V>
    {
        public EnumValue()
        {
            Key = new PEnum(typeof(E));
        }
    }

    public class KeyEnum<E, V> : KeyValue<V , PEnum>
    {
        public KeyEnum()
        {
            Value = new PEnum(typeof(E));
        }
    }

    public class EnumEnum<K, V> : EnumValue<PEnum, PEnum>
    {
        public EnumEnum()
        {
            Key = new PEnum(typeof(K));
            Value = new PEnum(typeof(V));
        }
    }

    #endregion
    #region Enum struct
    public class EnumInt<E> : EnumValue<E, int>{}
    [Serializable] public class EnumInt : EnumInt<Enum> { }

    public class EnumFloat<E> : EnumValue<E, float> { }
    [Serializable] public class EnumFloat : EnumFloat<Enum>{}

    public class EnumString<E> : EnumValue<E, string>{}
    [Serializable] public class EnumString : EnumString<Enum> { }

    public class EnumObject<E> : EnumValue<PEnum, UnityEngine.Object>{}
    [Serializable] public class EnumObject : EnumObject<Enum>{ }
    #endregion
    #region struct Enum
    [Serializable]
    public class IntEnum : KeyValue<int, PEnum>
    {
        public IntEnum(int key, PEnum value) : base(key, value) { }
    }

    [Serializable]
    public class FloatEnum : KeyValue<float, PEnum>
    {
        public FloatEnum(float key, PEnum value) : base(key, value) { }
    }

    [Serializable]
    public class StringEnum : KeyValue<string, PEnum>
    {
        public StringEnum(string key, PEnum value) : base(key, value) { }
    }

    [Serializable]
    public class ObjectEnum : KeyValue<UnityEngine.Object, PEnum>
    {
        public ObjectEnum(UnityEngine.Object key, PEnum value) : base(key, value) { }
    }
    #endregion
    #region Int Struct
    [Serializable]
    public class IntInt : KeyValue<int, int>
    {
    }

    [Serializable]
    public class IntFloat : KeyValue<int, float>
    {
        public IntFloat(int key, float value) : base(key, value)
        {
        }
    }

    [Serializable]
    public class IntString : KeyValue<int, string>
    {
        public IntString(int key, string value) : base(key, value)
        {
        }
    }

    [Serializable]
    public class IntObject : KeyValue<int, UnityEngine.Object>
    {
        public IntObject(int key, UnityEngine.Object value) : base(key, value)
        {
        }
    }
    #endregion
    #region Float Struct
    [Serializable]
    public class FloatInt : KeyValue<float, int>
    {
        public FloatInt(float key, int value) : base(key, value)
        {
        }
    }

    [Serializable]
    public class FloatFloat : KeyValue<float, float>
    {
        public FloatFloat(float key, float value) : base(key, value)
        {
        }
    }

    [Serializable]
    public class FloatString : KeyValue<float, string>
    {
        public FloatString(float key, string value) : base(key, value)
        {
        }
    }

    [Serializable]
    public class FloatObject : KeyValue<float, UnityEngine.Object>
    {
        public FloatObject(float key, UnityEngine.Object value) : base(key, value)
        {
        }
    }
    #endregion
    #region String Struct
    public class StringInt : KeyValue<string, int>
    {
        public StringInt(string key, int value) : base(key, value)
        {
        }
    }
    public class StringFloat : KeyValue<string, float>
    {
        public StringFloat(string key, float value) : base(key, value)
        {
        }
    }
    public class StringObject : KeyValue<string, UnityEngine.Object>
    {
        public StringObject(string key, UnityEngine.Object value) : base(key, value)
        {
        }
    }
    #endregion
    #region Object Struct
    [Serializable]
    public class ObjectInt : KeyValue<UnityEngine.Object, int>
    {
        public ObjectInt(UnityEngine.Object key, int value) : base(key, value)
        {
        }
    }

    [Serializable]
    public class ObjectFloat : KeyValue<UnityEngine.Object, float>
    {
        public ObjectFloat(UnityEngine.Object key, float value) : base(key, value)
        {
        }
    }

    [Serializable]
    public class ObjectString : KeyValue<UnityEngine.Object, string>
    {
        public ObjectString(UnityEngine.Object key, string value) : base(key, value)
        {
        }
    }
    #endregion
}