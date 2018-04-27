using ChainedRam.Core;
using ChainedRam.Core.Generation;
using ChainedRam.Core.Selection;
using ChainedRam.Inspecter.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ChainedRam.Inspecter.Generation
{
    //[CustomEditor(typeof(PoolGenerator), true)]
    public class PoolGeneratorEditor : NestedGeneratorEditor
    {
        //private SelectorType PrevSelectorType;

        //public override void OnInspectorGUI()
        //{
        //    EditorGUILayout.Space();
        //    EditorGUILayout.LabelField("Pool Generator Settings", EditorStyles.boldLabel);

        //    PoolGenerator pool = (PoolGenerator)target;

        //    DrawConditionSection(pool);

        //    base.OnInspectorGUI();
        //}

        //    #region Selector
        //    private void DrawConditionSection(PoolGenerator pool)
        //    {
        //        pool.SelectorType = (SelectorType)EditorGUILayout.EnumPopup("Selector Type", pool.SelectorType);

        //        ClearPreviousSelection(pool, PrevSelectorType);
        //        PrevSelectorType = pool.SelectorType;

        //        switch (pool.SelectorType)
        //        {
        //            case SelectorType.Other:
        //                pool.Selector = (Selector) EditorGUILayout.ObjectField("Selector", pool.Selector, typeof(Selector), true);
        //                break;
        //            case SelectorType.Order:
        //                OrderSelector order = pool.gameObject.GetComponent<OrderSelector>() ?? pool.gameObject.AddComponent<OrderSelector>();
        //                order.Repeat = EditorGUILayout.IntField("Repeat", order.Repeat);
        //                pool.Selector = order;
        //                var so = new SerializedObject(order);
        //                so.UpdateIfRequiredOrScript(); 
        //                //so.ApplyModifiedProperties(); 

        //                break;
        //            case SelectorType.Random:
        //                RandomSelector random = pool.gameObject.GetComponent<RandomSelector>() ?? pool.gameObject.AddComponent<RandomSelector>();
        //                random.NoRepeat = EditorGUILayout.Toggle("NoRepeat", random.NoRepeat);
        //                pool.Selector = random;
        //                break;
        //        }


        //    }

        //    private void ClearPreviousSelection(PoolGenerator pool, SelectorType prevConditionType)
        //    {
        //        if (prevConditionType == SelectorType.Other || pool.SelectorType == prevConditionType)
        //        {
        //            return;
        //        }

        //        pool.Selector = null;

        //        switch (prevConditionType)
        //        {
        //            case SelectorType.Order:
        //                DestroyImmediate(pool.GetComponent<OrderSelector>());
        //                break; 
        //            case SelectorType.Random:
        //                DestroyImmediate(pool.GetComponent<RandomSelector>());
        //                break; 
        //        }
        //    }
        //    #endregion
        //
    }
}
