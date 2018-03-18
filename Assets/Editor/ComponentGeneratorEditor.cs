using ChainedRam.Core.Generation;
using System;
using ChainedRam.Core.Enums;
using UnityEditor;


namespace ChainedRam.Inspecter.Generation
{
    //TODO: This needs some refactoring. This can be moved into the script itself Generator.OnGui >:\
    [CustomEditor(typeof(ComponentGenerator), true)]
    public class ComponentGeneratorEditor : Editor
    {
        private TerminationType PrevTerminationType;
        private GenerationType PrevConditionType;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField(target.name + " Generator Settings", EditorStyles.boldLabel);

            ComponentGenerator gen = ((ComponentGenerator)target);
            //gen.IsGenerating = EditorGUILayout.Toggle("IsGenerating", gen.IsGenerating);

            //gen.Delta = EditorGUILayout.PropertyField();

            DrawTerminatorSection(gen);
            DrawConditionSection(gen);
        }
        #region Terminator
        private void DrawTerminatorSection(ComponentGenerator gen)
        {
            gen.TerminatorType = (TerminationType)EditorGUILayout.EnumPopup("TerminatorType", gen.TerminatorType);
            ClearPreviousTerminators(gen, PrevTerminationType);

            switch (gen.TerminatorType)
            {
                case TerminationType.Internal:
                    gen.Terminator = null;
                    break;

                case TerminationType.External:
                    gen.Terminator = (GeneratorTerminatorComponent)EditorGUILayout.ObjectField("Terminator", gen.Terminator, typeof(GeneratorTerminatorComponent), true);
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

            PrevTerminationType = gen.TerminatorType;
        }

        private void ClearPreviousTerminators(ComponentGenerator gen, TerminationType prev)
        {
            if (prev == gen.TerminatorType || prev == TerminationType.Internal)
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
        private void DrawConditionSection(ComponentGenerator gen)
        {
            gen.GenerateConditionType = (GenerationType)EditorGUILayout.EnumPopup("ConditionType", gen.GenerateConditionType);

            ClearPreviousCondition(gen, PrevConditionType);

            switch (gen.GenerateConditionType)
            {
                case GenerationType.Internal:
                    gen.GenerateCondition = null;
                    break;

                case GenerationType.External:
                    gen.GenerateCondition = (GeneratorConditionComponent)EditorGUILayout.ObjectField("GenerateCondition", gen.GenerateCondition, typeof(GeneratorConditionComponent), true);
                    EditorGUILayout.Space();
                    if (gen.GenerateCondition != null)
                    {
                        CreateEditor(gen.GenerateCondition).DrawDefaultInspector();
                    }
                    break;
                case GenerationType.Cooldown:
                    CooldownCondition IntervalCond = gen.gameObject.GetComponent<CooldownCondition>() ?? gen.gameObject.AddComponent<CooldownCondition>();
                    IntervalCond.WaitTime = EditorGUILayout.Slider("Cooldown", IntervalCond.WaitTime, 0, 100);
                    gen.GenerateCondition = IntervalCond;
                    break;
            }

            PrevConditionType = gen.GenerateConditionType;
        }

        private void ClearPreviousCondition(ComponentGenerator gen, GenerationType prevConditionType)
        {
            if (prevConditionType == gen.GenerateConditionType || prevConditionType == GenerationType.Internal)
            {
                return;
            }

            gen.GenerateCondition = null;
            switch (prevConditionType)
            {
                case GenerationType.Cooldown:
                    DestroyImmediate(gen.gameObject.GetComponent<CooldownCondition>());
                    break;
            }
        }
        #endregion
    }
}



