using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GeneratorWrap), true)]
public class GeneratorWrapEditor : Editor
{
    bool showChildInspecter; 

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space();
        if (showChildInspecter = EditorGUILayout.Toggle("Show Wrapped Inspector", showChildInspecter))
        {
            ComponentGenerator gen = ((GeneratorWrap)target).Wrapped;

            if (gen == null)
            {
                EditorGUILayout.LabelField("Wrap not set");
            }
            else
            { 
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("'" + gen.name + "' Inspector", EditorStyles.boldLabel); 

                Editor drawer = CreateEditor(gen);
                drawer.OnInspectorGUI();
            }
        }
    }
}
