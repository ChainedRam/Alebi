using ChainedRam.Core.Collection;
using ChainedRam.Core.Enums;
using System;
using UnityEditor;
using UnityEngine;


//Source: https://docs.unity3d.com/ScriptReference/PropertyDrawer.html
[CustomPropertyDrawer(typeof(KeyValue<PEnum, PEnum>), true)]
[CustomPropertyDrawer(typeof(KeyValue<PEnum, Component>), true)]
[CustomPropertyDrawer(typeof(KeyValue<Component, PEnum>), true)]

[CustomPropertyDrawer(typeof(KeyValue<string, string>), true)]
[CustomPropertyDrawer(typeof(KeyValue<PEnum, string>), true)]
[CustomPropertyDrawer(typeof(KeyValue<string, PEnum>), true)]

[CustomPropertyDrawer(typeof(KeyValue<float, float>), true)]
[CustomPropertyDrawer(typeof(KeyValue<PEnum, float>), true)]
[CustomPropertyDrawer(typeof(KeyValue<float, PEnum>), true)]

[CustomPropertyDrawer(typeof(KeyValue<int, int>), true)]
[CustomPropertyDrawer(typeof(KeyValue<PEnum, int>), true)]
[CustomPropertyDrawer(typeof(KeyValue<int, PEnum>), true)]
public class KeyValueEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var KeyRect = new Rect(position.x, position.y, position.width/2, position.height);
        var ValueRect = new Rect(position.x + position.width/2, position.y, position.width / 2, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(KeyRect, property.FindPropertyRelative("Key"), GUIContent.none, true);

        EditorGUI.PropertyField(ValueRect, property.FindPropertyRelative("Value"), GUIContent.none, true);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
