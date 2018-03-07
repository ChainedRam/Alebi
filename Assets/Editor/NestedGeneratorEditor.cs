using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace ChainedRam.Inspecter.Generation
{
    [CustomEditor(typeof(NestedGenerator), true)]
    public class NestedGeneratorEditor : Editor
    {
        bool[] showChildrenInspecters; 
        //int children = 0;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();


            //EditorGUILayout.Space();
            //EditorGUILayout.LabelField("Nested Generator Settings", EditorStyles.boldLabel);

            //NestedGenerator ng = (NestedGenerator)target;

           
            //var serializedObject = new SerializedObject(target);
            //var property = serializedObject.FindProperty("Generators");

            //serializedObject.Update();
            //EditorGUILayout.PropertyField(property, true);
            //serializedObject.ApplyModifiedProperties();

            //Debug.Log(property.arraySize);
            //if (ng.ShowChildrenInspecter = EditorGUILayout.Toggle("Show Nested Inspectors", ng.ShowChildrenInspecter))
            //{
            //    //list children 
            //    DrawNestedChildren(ng);

            //    //draw each child inspecter
            //    DrawChildrenInspecter(ng); 
            //}
        }

        private void DrawNestedChildren(NestedGenerator ng)
        {
            //int prev = showChildrenInspecters?.Length?? 0;
           int sizeValue = EditorGUILayout.IntField("Children size", ng.ChildGenerators?.Length ?? 0);
            
            if (sizeValue <= 0)
            {
                sizeValue = 0;
                showChildrenInspecters = new bool[0];
                ng.ChildGenerators = new Generator[0];
            }

            //size changed 
            if (sizeValue != (showChildrenInspecters?.Length?? 0))
            {
                serializedObject.Update();
                Generator[] prevArray = ng.ChildGenerators;
                //bool[] prevShow = showChildrenInspecters; 

                ng.ChildGenerators = new Generator[sizeValue];
                showChildrenInspecters = new bool[sizeValue]; 

                for (int j = 0; j < sizeValue && j < prevArray.Length; j++)
                {
                    ng.ChildGenerators[j] = prevArray[j];
                    showChildrenInspecters[j] = false;
                }
                serializedObject.ApplyModifiedProperties();
            }

            //draw children
            int i = 0;
            if (sizeValue > 0)
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
                    //change color and store previous
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
                        //ask to show inspecter
                        if ((showChildrenInspecters[index] = EditorGUILayout.Toggle("Show " + child.name + " Inspecter", showChildrenInspecters[index])))
                        {
                            EditorGUILayout.Space();

                            EditorGUI.BeginDisabledGroup(true);
                            EditorGUILayout.ObjectField("Object source", child, typeof(Generator), true);
                            EditorGUI.EndDisabledGroup();

                            Editor drawer = CreateEditor(child);
                            drawer.OnInspectorGUI();
                            EditorGUILayout.Space();
                         }
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

