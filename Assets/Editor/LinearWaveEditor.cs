using ChainedRam.Alebi.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LinearWave))]
public class LinearWaveEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Set up Patterns"))
        {
            LinearWave linearWave = (LinearWave)target;

            int i= 0; 
            foreach (Pattern p in linearWave.patterns)
            {
                if (p != null)
                {
                    linearWave.SetUpPattern(i++, p);
                }
            }

        

        }

    }
}
