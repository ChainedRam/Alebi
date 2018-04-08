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
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        //parts width
        float dw = position.width / 3 ;
        float ow = position.width / 3;
        float fw = position.width - (dw + ow);
        float personalSpace = 2f; 
        // Calculate rects
        var directionRect = new Rect(position.x, position.y, dw, position.height);
        var offsetRect = new Rect(position.x + dw, position.y, ow, position.height);
        var thirdsRect = new Rect(position.x + dw + ow + personalSpace, position.y, fw - personalSpace, position.height);

        EditorGUI.PropertyField(directionRect, property.FindPropertyRelative("Direction"), GUIContent.none);

        EditorGUI.PropertyField(offsetRect, property.FindPropertyRelative("Offset"), GUIContent.none);

        switch ((Direction)property.FindPropertyRelative("Direction").intValue)
        {
            case Direction.Transform:
                EditorGUI.PropertyField(thirdsRect, property.FindPropertyRelative("Refrence"), GUIContent.none);
                break; 
            case Direction.Center: 
                //doesn't need relativeTo
                break;
            default:
                EditorGUI.PropertyField(thirdsRect, property.FindPropertyRelative("RelativeTo"), GUIContent.none);
                break; 
        }

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
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
