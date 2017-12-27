using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexedEmotion : MonoBehaviour {

    public int Index;
    public string Emotion;

    public IndexedEmotion Init(int i, string e)
    {
        Index = i;
        Emotion = e;

        return this; 
    }
}
