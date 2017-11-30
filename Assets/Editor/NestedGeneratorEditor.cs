using ChainedRam.Alebi.Battle;
using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace ChainedRam.Core.Generation
{
    [CustomEditor(typeof(NestedGenerator), true)]
    public class NestedGeneratorEditor : GeneratorEditor
    {

        int children = 0;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Nested Generator Settings", EditorStyles.boldLabel);

            NestedGenerator ng = (NestedGenerator)target;

            if (ng.ShowChildrenInspecter = EditorGUILayout.Toggle("Show Nested Inspectors", ng.ShowChildrenInspecter))
            {
                //list children 
                DrawNestedChildren(ng);

                //draw each child inspecter
                DrawChildrenInspecter(ng); 
            }
        }

        private void DrawNestedChildren(NestedGenerator ng)
        {
            int prev = children;
            children = EditorGUILayout.IntField("Children size", ng.ChildGenerators?.Length ?? children);
            
            if (children < 0)
            {
                children = 0;
            }

            //size changed 
            if (prev != children)
            {
                serializedObject.Update();
                Generator[] prevArray = ng.ChildGenerators;
                ng.ChildGenerators = new Generator[children];
                
                for (int j = 0; j < children && j < prevArray?.Length; j++)
                {
                    ng.ChildGenerators[j] = prevArray[j];
                }
                serializedObject.ApplyModifiedProperties();
            }

            //draw children
            int i = 0;
            if (children > 0)
            {
                foreach (var item in ng.ChildGenerators)
                {
                    ng.ChildGenerators[i] = (Generator)EditorGUILayout.ObjectField("Generator " + i, ng.ChildGenerators[i], typeof(Generator), true);
                    i++;
                }
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void DrawChildrenInspecter(NestedGenerator ng)
        {
            if (ng.ChildGenerators == null || ng.ChildGenerators.Length == 0)
            {
                EditorGUILayout.LabelField("No childern");
            }
            else
            {
                EditorGUILayout.LabelField(ng.name + " Children Inspecter Begin", EditorStyles.boldLabel);
                int index = 0;
                foreach (var child in ng.ChildGenerators)
                {
                    Color prevLabelColor = EditorStyles.label.normal.textColor;
                    Color prevFontColor = EditorStyles.boldLabel.normal.textColor;

                    EditorStyles.label.normal.textColor = SelectColor(index);
                    EditorStyles.boldLabel.normal.textColor = SelectColor(index);

                    if (child == null)
                    {
                        EditorGUILayout.LabelField((index) + "-  Field is null");
                    }
                    else
                    {
                        EditorGUILayout.Space();

                        EditorGUI.BeginDisabledGroup(true);
                        EditorGUILayout.ObjectField("Object source", child, typeof(Generator), true);
                        EditorGUI.EndDisabledGroup();

                        Editor drawer = CreateEditor(child);
                        drawer.OnInspectorGUI();
                        EditorGUILayout.Space();
                    }
                    EditorStyles.label.normal.textColor = prevLabelColor;
                    EditorStyles.boldLabel.normal.textColor = prevFontColor; 

                    ++index;
                }
                EditorGUILayout.LabelField(ng.name + " Children Inspecter End", EditorStyles.boldLabel);
            }
        }

        private Color SelectColor(int i)
        {
            Color[] colors = { Color.red, Color.blue, Color.magenta , new Color32(0, 128, 0, 255), new Color32(128, 128, 0, 255)};

            Color selected = colors[i % colors.Length];

            return selected;
            
        }
    }
}

/**
    
 */
