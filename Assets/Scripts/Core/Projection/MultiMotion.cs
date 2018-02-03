using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiMotion : Motion
{

    public Motion[] motions;

    public override Vector2 GetOffset()
    {
        Vector2 fakeVector = new Vector2(0, 0);
        foreach (var m in motions)
        {
            fakeVector = m.Apply(fakeVector);
        }
        return fakeVector;
    }

    public override void Initialize()
    {
        base.Initialize();
        foreach (var motion in motions)
        {
            motion.Initialize();
        }
    }
}
