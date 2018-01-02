using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Generator), true)]
public class GeneratorEditor : Editor
{
    private GenTermination PrevTerminationType;
    private GenCondition PrevConditionType;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        /*
        //draw script source
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));
        EditorGUI.EndDisabledGroup();
        */

        EditorGUILayout.Space();

        Generator gen = ((Generator)target);
        
        //draw header
        EditorGUILayout.LabelField(gen.name + " Generator Settings", EditorStyles.boldLabel);

        gen.IsGenerating = EditorGUILayout.Toggle("IsGenerating", gen.IsGenerating);
       
        DrawTerminatorSection(gen);

        //EditorGUILayout.Space();
        DrawConditionSection(gen); 
    }
    #region Terminator
    private void DrawTerminatorSection(Generator gen)
    {
        gen.TerminatorTag = (GenTermination)EditorGUILayout.EnumPopup("TerminatorType", gen.TerminatorTag);
        ClearPreviousTerminators(gen, PrevTerminationType);

        switch (gen.TerminatorTag)
        {
            case GenTermination.Internal:
                gen.Terminator = null; 
                break;

            case GenTermination.External:
                gen.Terminator = (GeneratorTerminator) EditorGUILayout.ObjectField("Terminator", gen.Terminator, typeof(GeneratorTerminator), true);
                EditorGUILayout.Space();
                if (gen.Terminator != null)
                {
                    try
                    {
                        CreateEditor(gen.Terminator).DrawDefaultInspector();
                       
                    }
                    catch(NullReferenceException)
                    {
                        //is ok. just don't draw it.
                    }
                }
                break;

            case GenTermination.Duration:
                DurationTerminator TimedTermnator = gen.gameObject.GetComponent<DurationTerminator>() ?? gen.gameObject.AddComponent<DurationTerminator>();
                TimedTermnator.Duration = EditorGUILayout.FloatField("Duration", TimedTermnator.Duration);
                gen.Terminator = TimedTermnator;
                break;

            case GenTermination.Counter:
                CountTerminator CountTerminator = gen.gameObject.GetComponent<CountTerminator>() ?? gen.gameObject.AddComponent<CountTerminator>();
                CountTerminator.MaxGeneration = EditorGUILayout.IntField("Max Count", CountTerminator.MaxGeneration);
                gen.Terminator = CountTerminator;
                break;

            case GenTermination.OnSkip:
                SkippedTerminator SkipTerminator = gen.gameObject.GetComponent<SkippedTerminator>() ?? gen.gameObject.AddComponent<SkippedTerminator>();
                gen.Terminator = SkipTerminator;
                break;
        }

        PrevTerminationType = gen.TerminatorTag;
    }

    private void ClearPreviousTerminators(Generator gen, GenTermination prev)
    {
        if (prev == gen.TerminatorTag || prev == GenTermination.Internal)
        {
            return;
        }

        gen.Terminator = null;
        switch (prev)
        {
            case GenTermination.Duration:
                DestroyImmediate(gen.gameObject.GetComponent<DurationTerminator>());
                break;
            case GenTermination.Counter:
                DestroyImmediate(gen.gameObject.GetComponent<CountTerminator>());
                break;
            case GenTermination.OnSkip:
                DestroyImmediate(gen.gameObject.GetComponent<SkippedTerminator>());
                break;
        }
    }
    #endregion
    #region Condition
    private void DrawConditionSection(Generator gen)
    {
        gen.GenerateConditionTag = (GenCondition)EditorGUILayout.EnumPopup("ConditionType", gen.GenerateConditionTag);

        ClearPreviousCondition(gen, PrevConditionType);

        switch (gen.GenerateConditionTag)
        {
            case GenCondition.Internal:
                gen.GenerateCondition = null;
                break;

            case GenCondition.External:
                gen.GenerateCondition = (GeneratorCondition)EditorGUILayout.ObjectField("GenerateCondition", gen.GenerateCondition, typeof(GeneratorCondition), true);
                EditorGUILayout.Space();
                if (gen.GenerateCondition != null)
                {
                    CreateEditor(gen.GenerateCondition).DrawDefaultInspector();
                }
                break;
            case GenCondition.Cooldown:
                CooldownCondition IntervalCond = gen.gameObject.GetComponent<CooldownCondition>() ?? gen.gameObject.AddComponent<CooldownCondition>();
                IntervalCond.WaitTime = EditorGUILayout.Slider("Cooldown", IntervalCond.WaitTime, 0, 100); 
                gen.GenerateCondition = IntervalCond;
                break;
        }

        PrevConditionType = gen.GenerateConditionTag; 
    }

    private void ClearPreviousCondition(Generator gen, GenCondition prevConditionType)
    {
        if (prevConditionType == gen.GenerateConditionTag || prevConditionType == GenCondition.Internal)
        {
            return;
        }

        gen.GenerateCondition = null;
        switch (prevConditionType)
        {
            case GenCondition.Cooldown:
                DestroyImmediate(gen.gameObject.GetComponent<CooldownCondition>());
                break;
        }
    }
    #endregion
}



