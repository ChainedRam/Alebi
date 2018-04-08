using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Projectile), true)]
public class MotionEditor : Editor
{
    private bool ShowMotion; 
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        

        Projectile gen = ((Projectile)target);

        if(gen.Motion != null && (ShowMotion = EditorGUILayout.Toggle("Show Motion Inspecter", ShowMotion)))
        {
            EditorGUILayout.LabelField(gen.Motion.name +  " Inspecter");
            CreateEditor(gen.Motion).DrawDefaultInspector();    
        }

    }

}
