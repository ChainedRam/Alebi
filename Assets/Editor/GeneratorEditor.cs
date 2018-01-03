using ChainedRam.Core.Generation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ChainedRam.Inspecter.Generation
{

    [CustomEditor(typeof(Generator), true)]
    public class GeneratorEditor : Editor
    {
        private TerminationType PrevTerminationType;
        private GeneratingType PrevConditionType;

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
            gen.TerminatorTag = (TerminationType)EditorGUILayout.EnumPopup("TerminatorType", gen.TerminatorTag);
            ClearPreviousTerminators(gen, PrevTerminationType);

            switch (gen.TerminatorTag)
            {
                case TerminationType.Internal:
                    gen.Terminator = null;
                    break;

                case TerminationType.External:
                    gen.Terminator = (GeneratorTerminator)EditorGUILayout.ObjectField("Terminator", gen.Terminator, typeof(GeneratorTerminator), true);
                    EditorGUILayout.Space();
                    if (gen.Terminator != null)
                    {
                        try
                        {
                            CreateEditor(gen.Terminator).DrawDefaultInspector();

                        }
                        catch (NullReferenceException)
                        {
                            //is ok. just don't draw it.
                        }
                    }
                    break;

                case TerminationType.Duration:
                    DurationTerminator TimedTermnator = gen.gameObject.GetComponent<DurationTerminator>() ?? gen.gameObject.AddComponent<DurationTerminator>();
                    TimedTermnator.Duration = EditorGUILayout.FloatField("Duration", TimedTermnator.Duration);
                    gen.Terminator = TimedTermnator;
                    break;

                case TerminationType.Counter:
                    CountTerminator CountTerminator = gen.gameObject.GetComponent<CountTerminator>() ?? gen.gameObject.AddComponent<CountTerminator>();
                    CountTerminator.MaxGeneration = EditorGUILayout.IntField("Max Count", CountTerminator.MaxGeneration);
                    gen.Terminator = CountTerminator;
                    break;

                case TerminationType.OnSkip:
                    SkippedTerminator SkipTerminator = gen.gameObject.GetComponent<SkippedTerminator>() ?? gen.gameObject.AddComponent<SkippedTerminator>();
                    gen.Terminator = SkipTerminator;
                    break;
            }

            PrevTerminationType = gen.TerminatorTag;
        }

        private void ClearPreviousTerminators(Generator gen, TerminationType prev)
        {
            if (prev == gen.TerminatorTag || prev == TerminationType.Internal)
            {
                return;
            }

            gen.Terminator = null;
            switch (prev)
            {
                case TerminationType.Duration:
                    DestroyImmediate(gen.gameObject.GetComponent<DurationTerminator>());
                    break;
                case TerminationType.Counter:
                    DestroyImmediate(gen.gameObject.GetComponent<CountTerminator>());
                    break;
                case TerminationType.OnSkip:
                    DestroyImmediate(gen.gameObject.GetComponent<SkippedTerminator>());
                    break;
            }
        }
        #endregion
        #region Condition
        private void DrawConditionSection(Generator gen)
        {
            gen.GenerateConditionTag = (GeneratingType)EditorGUILayout.EnumPopup("ConditionType", gen.GenerateConditionTag);

            ClearPreviousCondition(gen, PrevConditionType);

            switch (gen.GenerateConditionTag)
            {
                case GeneratingType.Internal:
                    gen.GenerateCondition = null;
                    break;

                case GeneratingType.External:
                    gen.GenerateCondition = (GeneratorCondition)EditorGUILayout.ObjectField("GenerateCondition", gen.GenerateCondition, typeof(GeneratorCondition), true);
                    EditorGUILayout.Space();
                    if (gen.GenerateCondition != null)
                    {
                        CreateEditor(gen.GenerateCondition).DrawDefaultInspector();
                    }
                    break;
                case GeneratingType.Cooldown:
                    CooldownCondition IntervalCond = gen.gameObject.GetComponent<CooldownCondition>() ?? gen.gameObject.AddComponent<CooldownCondition>();
                    IntervalCond.WaitTime = EditorGUILayout.Slider("Cooldown", IntervalCond.WaitTime, 0, 100);
                    gen.GenerateCondition = IntervalCond;
                    break;
            }

            PrevConditionType = gen.GenerateConditionTag;
        }

        private void ClearPreviousCondition(Generator gen, GeneratingType prevConditionType)
        {
            if (prevConditionType == gen.GenerateConditionTag || prevConditionType == GeneratingType.Internal)
            {
                return;
            }

            gen.GenerateCondition = null;
            switch (prevConditionType)
            {
                case GeneratingType.Cooldown:
                    DestroyImmediate(gen.gameObject.GetComponent<CooldownCondition>());
                    break;
            }
        }
        #endregion
    }
}



