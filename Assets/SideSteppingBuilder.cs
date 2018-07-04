using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSteppingBuilder : OnceGenerator
{
    public ShowupOnScreenGenerator Enter;
    public ShowupOnScreenGenerator Exit;
    public TimedGemGenerator TimedGemGenerator;
    private int Rank;

    protected override void Start()
    {
        base.Start();
        Rank = 0;
    }

    protected override void OnGenerate(GenerateEventArgs e)
    {
        if (Rank % 2 == 1) {
            Enter.PositionProvider.SetPositionOffset(new Vector2(-4.5f, 1f));
            Exit.PositionProvider.SetPositionOffset(new Vector2(-4.5f, 1f));
        }
        else 
        {
            Enter.PositionProvider.SetPositionOffset(new Vector2(4.5f, 1f));
            Exit.PositionProvider.SetPositionOffset(new Vector2(4.5f, 1f));
        }
        TimedGemGenerator.ShardCount = Rank + 5;
        Rank++;
    }
}

