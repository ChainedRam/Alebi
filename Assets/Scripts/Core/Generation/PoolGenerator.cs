using ChainedRam.Core.Generation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolGenerator : NestedGenerator
{
    [Header("Pool Geenerator Settings")]

    public Generator Selected;

    public Selector Selector;

    /// <summary>
    /// 
    /// </summary>
    protected override void SetupGenerator()
    {
        //ignore NestedGeneratorSetup
        OnGenerate += () => SwitchIn(Selector.Select(ChildGenerators, Selected));
    }

    public void SwitchIn(Generator gen)
    {
        //Debug.Log("Switching " + Selected.name + " with " + gen.name );
        if(Selected != null)
        {
            Demote(Selected); 
        }

        Promote(gen); 
    }

    public void Promote(Generator gen)
    {
       // Debug.Log("Promoting " + gen.name);
        
        Selected = gen; 
        Attach(gen);
        gen.StartGenerating(); 
    }

    public void Demote(Generator gen)
    {
        //Debug.Log("Demoting " + gen.name);
        gen.StopGenerating();
        Detach(gen);

        Selected = null; 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override bool ShouldGenerate()
    {
        return (Options == ShouldGenerateOptions.Or? true : (Selected?.ShouldGenerate() ?? true)); 
    }

    protected override void SkippedGeneration()
    {
       
    }
}

/// <summary>
/// Generic version of PoolGenerator
/// </summary>
/// <typeparam name="T"></typeparam>
public class PoolGenerator<T> : PoolGenerator where T : Generator
{
    public T[] Pool;

    public override Generator[] ChildGenerators
    {
        get
        {
            return Pool;
        }
    }

}
