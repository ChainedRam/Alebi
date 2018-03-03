using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProjectileGeneration))]
public class ProjectileGenerationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ProjectileGeneration projectileGeneration = (ProjectileGeneration) target;
        LineMotion motion = projectileGeneration.GetComponent<LineMotion>(); /* ?? projectileGeneration.gameObject.AddComponent<LineMotion>();*/

        if (motion != null)
        {
            Editor editor = CreateEditor(motion);

            editor.DrawDefaultInspector();
            projectileGeneration.Motion = motion;
        }
    }

}
