using ChainedRam.Alebi.Battle;
using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NestedGenerator))]
public class NestedGeneratorEditor : Editor
{  
    bool showNested;

    int children =0; 

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        NestedGenerator ng = (NestedGenerator)target;

        int prev = children; 
        children = EditorGUILayout.IntField("Children size", ng.ChildGenerators?.Length?? 0);

        if(children < 0)
        {
            children = 0; 
        }

        //size changed 
        if (prev != children)
        {
            Generator[] prevArray = ng.ChildGenerators; 
            ng.ChildGenerators = new Generator[children];

            for (int j = 0; j < children && j < prevArray.Length; j++)
            {
                ng.ChildGenerators[j] = prevArray[j]; 
            }
        }

        //draw children
        int i = 0; 
        foreach (var item in ng.ChildGenerators)
        {
            ng.ChildGenerators[i] = (Generator) EditorGUILayout.ObjectField("Generator "+(i+1), ng.ChildGenerators[i], typeof(Generator), true);
            i++;
        }

        serializedObject.Update();

        if (showNested = EditorGUILayout.Toggle("Show Nested Inspectors", showNested))
        {
            if (ng.ChildGenerators == null || ng.ChildGenerators.Length == 0)
            {
                EditorGUILayout.LabelField("No childern");
            }
            else
            {
                int index = 0;
                foreach (var child in ng.ChildGenerators)
                {
                    ++index;
                    if (child == null)
                    {
                        EditorGUILayout.LabelField((index) + "-  Field is null"); 
                        continue;
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField((index) + "- " + child.name + " Settings");

                    Editor drawer = CreateEditor(child);
                    drawer.OnInspectorGUI();
                }
            }
        }
    }
}

/**
    
 */
