using ChainedRam.Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionEffect : MonoBehaviour
{
    public abstract void OnHit(Player p);
}
