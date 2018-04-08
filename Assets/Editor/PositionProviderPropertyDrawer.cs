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

        DrawPositionProperties(position, property, 0);
        DrawOffsetProperties(position, property, 1);

        DrawRotationProperties(position, property, 2); 

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
    private void DrawPositionProperties(Rect position, SerializedProperty property, int i)
    {
        //parts width
        float width1 = position.width / 2;
        float lastWidth = position.width - (width1);
        float personalSpace = 2f;

        // Calculate rects
        var directionRect = new Rect(position.x, position.y, width1, LineHeight);
        var lastRect = new Rect(position.x + width1 + personalSpace, position.y, lastWidth - personalSpace, LineHeight);

        EditorGUI.PropertyField(directionRect, property.FindPropertyRelative("Direction"), GUIContent.none);

        switch ((Direction)property.FindPropertyRelative("Direction").intValue)
        {
            case Direction.Transform:
                EditorGUI.PropertyField(lastRect, property.FindPropertyRelative("PositionRefrence"), GUIContent.none);
                break;
            case Direction.Center:
                //doesn't need relativeTo
                break;
            default:
                EditorGUI.PropertyField(lastRect, property.FindPropertyRelative("RelativeTo"), GUIContent.none);
                break;
        }
    }

    private void DrawOffsetProperties(Rect position, SerializedProperty property, int i)
    {
        float width1 = Math.Min(position.width / 2, 60);
        float lastWidth = position.width - (width1);

        var labelRect = new Rect(position.x, position.y + i * (LineHeight + personalSpace), width1, LineHeight);
        var offsetRect = new Rect(position.x + width1, position.y + i * (LineHeight + personalSpace), lastWidth, LineHeight);

        EditorGUI.LabelField(labelRect, "Offset");
        EditorGUI.PropertyField(offsetRect, property.FindPropertyRelative("Offset"), GUIContent.none);
    }

    private void DrawRotationProperties(Rect position, SerializedProperty property, int i)
    {
        //parts width
        float width1 = Math.Min(position.width / 3, 60);
        float width2 = (2 * position.width / 3) - width1;
        float width3 = position.width - (width1 + width2);
        
        // Calculate rects
        var firstRect = new Rect(position.x, position.y + i * (LineHeight + personalSpace), width1, LineHeight - personalSpace);
        var secondRect = new Rect(position.x + width1, position.y + i * (LineHeight + personalSpace), width2, LineHeight- personalSpace);
        var lastRect = new Rect(position.x + width1 + width2 + personalSpace, position.y  + i * (LineHeight + personalSpace), width3 - personalSpace, LineHeight - personalSpace);

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
        return (LineHeight + personalSpace) * LineCount; 
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
