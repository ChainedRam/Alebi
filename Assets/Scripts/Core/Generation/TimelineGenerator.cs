using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineGenerator : Generator
{
    public List<TimedGenerator> TimedGeneration;

    private IEnumerator<TimedGenerator> Enumerator;

    private float CurrentTime; 

    protected override void OnBegin()
    {
        base.OnBegin();
        CurrentTime = 0; 
        Enumerator = TimedGeneration.GetEnumerator();
        Enumerator.Reset();
        Enumerator.MoveNext(); 
    }

    protected override void OnGenerate(GenerateEventArgs e)
    {
        if(Enumerator.Current != null)
        {
            Enumerator.Current.Generate();
            CurrentTime = Enumerator.Current.WaitTime;
            Enumerator.MoveNext(); 
        }
    }

    protected override bool ShouldGenerate()
    {
        return CurrentTime <= 0;
    }

    protected override bool ShouldTerminate()
    {
        return Enumerator.Current == null && CurrentTime <= 0;
    }

    protected override void Update()
    {
        base.Update(); 
        if (CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
        }
    }
}
