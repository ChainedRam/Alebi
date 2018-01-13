using ChainedRam.Core.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(PEnum),true)]
public class EPnumEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        object obj; 

        try
        {
           UnityEngine.Object fucker = property.serializedObject.targetObject;

            obj = GetFieldViaPath(fucker.GetType(), property.propertyPath, fucker);
        }
        catch(Exception e)
        {
            Debug.Log("Error " + e.StackTrace);
            return;
        }

        PEnum t = obj as PEnum; 
        t.Value = EditorGUI.EnumPopup(position, label, t.Value);

        EditorGUI.EndProperty();
    }

    public static object GetFieldViaPath(System.Type type, string path, object obj)
    {
        Type parentType = type;
        FieldInfo fi = type.GetField(path);
        int i= -1; 
        string[] perDot = path.Split('.');

        foreach (string fieldName in perDot)
        {
            if(i >= 0)
            {
                i = int.Parse(fieldName.Substring(fieldName.IndexOf('[')+1, fieldName.Length - fieldName.IndexOf(']')));

                obj = ((object[])obj)[i];

                i = -1;

                parentType = obj.GetType();
                continue; 
            }

            if (parentType.IsArray)
            {
                i = 0;
                continue; 
            }

            fi = parentType.GetField(fieldName);
            obj = fi.GetValue(obj);
            

            if (fi == null)
            {
                return null;
            }

            parentType = fi.FieldType;
        }

        return  obj; 
    }
}