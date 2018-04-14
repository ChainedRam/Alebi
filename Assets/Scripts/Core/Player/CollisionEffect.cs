﻿using ChainedRam.Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionEffect<T> : MonoBehaviour where T : Component
{
    public abstract void OnHit(T t);
}
