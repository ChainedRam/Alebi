using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Extentions
{
    public static class Vector2Extention
    {
        /// <summary>
        /// Rotates the vector based on given degree. 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static Vector2 Rotate(this Vector2 v, float degree)
        {
            float sin = Mathf.Sin(degree * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degree * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;

            v.x = (cos * tx) - (sin * ty);
            v.y = (sin * tx) + (cos * ty);
            return v;
        }
    }
}

