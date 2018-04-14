using ChainedRam.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(PositionProvider))]
public class PositionProviderPropertyDrawer : PropertyDrawer
{
    private float LineHeight;
    private const int LineCount = 3;

    private const float personalSpace = 1.5f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;


        var firstRect = new Rect(position.x, position.y, position.width, position.height - 2 * LineHeight); 
        DrawPositionProperties(firstRect, property);

        var secondRect = new Rect(position.x, firstRect.yMax, position.width, LineHeight);
        DrawOffsetProperties(secondRect, property);

        var thordRect = new Rect(position.x, secondRect.yMax, position.width, LineHeight);
        DrawRotationProperties(thordRect, property); 

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
    private void DrawPositionProperties(Rect position, SerializedProperty property)
    {
        int parts = 2; 
        
        //parts width
        float width1 = Mathf.Min(70, position.width / parts);

        float lastWidth = position.width - (width1);
        float personalSpace = 2f;

        // Calculate rects
        var firstRect = new Rect(position.x, position.y, width1, LineHeight);
        var lastRect = new Rect(position.x + width1 + personalSpace, position.y, lastWidth - personalSpace, LineHeight);

        EditorGUI.PropertyField(firstRect, property.FindPropertyRelative("Location"), GUIContent.none);

        switch ((PositionLocation)property.FindPropertyRelative("Location").intValue)
        {
            case PositionLocation.Transform:
                EditorGUI.PropertyField(lastRect, property.FindPropertyRelative("PositionRefrence"), GUIContent.none);
                break;
            case PositionLocation.Direction:
                //Direction enumValue = (Direction) property.FindPropertyRelative("Direction").intValue;
                //Enum value = EditorGUI.EnumPopup(lastRect,enumValue);
                //property.FindPropertyRelative("Direction").intValue = Convert.ToInt32(value);
                var directionRect = new Rect(position.x + width1, position.y, lastWidth / 2, LineHeight);
                var relativeToRect = new Rect(position.x + width1 + lastWidth / 2, position.y, lastWidth / 2, LineHeight);

                EditorGUI.PropertyField(directionRect, property.FindPropertyRelative("Direction"), GUIContent.none);
                EditorGUI.PropertyField(relativeToRect, property.FindPropertyRelative("RelativeTo"), GUIContent.none);
                break;
            case PositionLocation.Random:

                var randomMultRect = new Rect(position.x + width1, position.y, lastWidth / 2, LineHeight);
                var randPositionRect = new Rect(position.x + width1 + lastWidth / 2, position.y, lastWidth / 2, LineHeight);

                EditorGUI.PropertyField(randomMultRect, property.FindPropertyRelative("RandomMultitude"), GUIContent.none);
                
                //EditorGUI.PropertyField(randPositionRect, property.FindPropertyRelative("RandomPositionOption"), GUIContent.none);
                RandomPositionOption enumValue = (RandomPositionOption) property.FindPropertyRelative("RandomPositionOption").intValue;
                Enum value = EditorGUI.EnumMaskPopup(randPositionRect, GUIContent.none, enumValue);
                property.FindPropertyRelative("RandomPositionOption").intValue = Convert.ToInt32(value);

                //Debug.Log(Convert.ToInt32(value));
                //Debug.Log("LOL" + (int)RandomPositionOption.Transforms);
                if ((Convert.ToInt32(value) & (int)RandomPositionOption.Transforms) == (int)RandomPositionOption.Transforms)
                {
                    //Debug.Log("I'm in");
                    var arryaRect = new Rect(position.x, position.y + LineHeight, position.width, LineHeight);

                    SerializedProperty list = property.FindPropertyRelative("RandomTransforms");

                    EditorGUI.PropertyField(arryaRect, list, new GUIContent("Transforms"));

                    if (list.isExpanded)
                    {
                        var arraySizeRect = new Rect(position.x, arryaRect.yMax, position.width, LineHeight);
                        list.arraySize = EditorGUI.IntField(arraySizeRect, list.arraySize);
                        for (int i = 0; i < list.arraySize; i++)
                        {
                            var elementRect = new Rect(position.x, arraySizeRect.yMax + LineHeight * i, position.width, LineHeight);
                            EditorGUI.PropertyField(elementRect, list.GetArrayElementAtIndex(i), GUIContent.none);
                        }
                    }

                }

                break;
        }
    }

    private void DrawOffsetProperties(Rect position, SerializedProperty property)
    {
        float width1 = Math.Min(position.width / 2, 60);
        float lastWidth = position.width - (width1);

        var labelRect = new Rect(position.x, position.y + personalSpace, width1, position.height);
        var offsetRect = new Rect(labelRect.xMax, position.y, lastWidth, position.height);

        EditorGUI.LabelField(labelRect, "Offset");
        EditorGUI.PropertyField(offsetRect, property.FindPropertyRelative("Offset"), GUIContent.none);
    }

    private void DrawRotationProperties(Rect position, SerializedProperty property)
    {
        //parts width
        float width1 = Math.Min(position.width / 3, 60);
        float width2 = (2 * position.width / 3) - width1;
        float width3 = position.width - (width1 + width2);
        
        // Calculate rects
        var firstRect = new Rect(position.x, position.y, width1, LineHeight - personalSpace);
        var secondRect = new Rect(position.x + width1, position.y, width2, LineHeight- personalSpace);
        var lastRect = new Rect(position.x + width1 + width2 + personalSpace, position.y, width3 - personalSpace, LineHeight - personalSpace);

        EditorGUI.LabelField(firstRect, "Rotation");
        EditorGUI.PropertyField(secondRect, property.FindPropertyRelative("RotationFacing"), GUIContent.none);

        switch ((RotationFacing)property.FindPropertyRelative("RotationFacing").intValue)
        {
            case RotationFacing.Numaric:
                EditorGUI.PropertyField(lastRect, property.FindPropertyRelative("Degree"), GUIContent.none);
                break;

            case RotationFacing.MatchTransform:
            case RotationFacing.FaceTransform:
                EditorGUI.PropertyField(lastRect, property.FindPropertyRelative("RotationRefrence"), GUIContent.none);
                break;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        LineHeight = base.GetPropertyHeight(property, label);

        float requestedHeight = (LineHeight + personalSpace) * LineCount;

        int flags = property.FindPropertyRelative("RandomPositionOption").intValue; 
        if ((flags & (int)RandomPositionOption.Transforms) == (int)RandomPositionOption.Transforms)
        {
            requestedHeight += 1 * LineHeight;

            if (property.FindPropertyRelative("RandomTransforms").isExpanded)
            {
                requestedHeight += LineHeight * (1 + property.FindPropertyRelative("RandomTransforms").arraySize);
            }
        }

        return requestedHeight;




    }
    public T GetActualObjectForSerializedProperty<T>(FieldInfo fieldInfo, SerializedProperty property) where T : class
    {
        var obj = fieldInfo.GetValue(property.serializedObject.targetObject);
        if (obj == null) { return null; }

        T actualObject = null;
        if (obj.GetType().IsArray)
        {
            var index = Convert.ToInt32(new string(property.propertyPath.Where(c => char.IsDigit(c)).ToArray()));
            actualObject = ((T[])obj)[index];
        }
        else
        {
            actualObject = obj as T;
        }
        return actualObject;
    }
}
