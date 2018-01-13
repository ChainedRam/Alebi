using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace ChainedRam.Core.Enums.Extensions
{
    public static class EnumExtensions
    {

        public static bool HasFlag<T>(this T property, T flag) where T : struct, IComparable, IFormattable, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Type must be an enum.");
            }

            int proeryValue = property.ToInt32(null);
            int flagValue = flag.ToInt32(null);


            return (proeryValue & flagValue) == flagValue;
        }

        public static void AddFlag<T>(ref T property, T flag) where T : struct, IComparable, IFormattable, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Type must be an enum.");
            }

            int proeryValue = property.ToInt32(null);
            int flagValue = flag.ToInt32(null);

            proeryValue = (proeryValue | flagValue);
            //todo

            property = (T)(object)proeryValue;
        }

        public static void RemoveFlag<T>(ref T property, T flag) where T : struct, IComparable, IFormattable, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Type must be an enum.");
            }

            int proeryValue = property.ToInt32(null);
            int flagValue = flag.ToInt32(null);

            proeryValue = proeryValue & ~flagValue;

            property = (T)(object)proeryValue;
        }


        //TODO needs Quick Maths! 
        public static void SetFlag<T>(ref T property, T flag, bool value) where T : struct, IComparable, IFormattable, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Type must be an enum.");
            }

            if (property.HasFlag(flag) == value)
            {
                return;
            }

            if (value)
            {
                AddFlag(ref property, flag);
            }
            else
            {
                RemoveFlag(ref property, flag);
            }
        }
    }
}
