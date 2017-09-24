using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Battle
{
    public class LinearWave : Wave
    {
        public bool IsCenter; 

        public Vector2 Offset;

        public Vector2 Spacing; 

        //Horizontal

        public override void SetUpPattern(int index, Pattern pattern)
        {
            pattern.gameObject.transform.localPosition = Offset + (Spacing * (IsCenter? ((float)patterns.Count-1)/2 - index : index)); 
        }
    }
}
