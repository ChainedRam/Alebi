using ChainedRam.Core.Extentions;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(UnityExtention))]
public class UnityExtentionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        AssetDatabase.Refresh();
        if (GUILayout.Button("Refresh C# Project"))
        {
            AssetDatabase.Refresh(); 
        }
    }
}
