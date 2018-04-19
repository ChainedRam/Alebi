using ChainedRam.Core;
using ChainedRam.Core.Extentions;
using ChainedRam.Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlayerCenterCollision : PlayerCollisionEffect
{
    public float PushPower;

    public override void OnHit(Player player)
    {
        float angle = PositionProvider.AngleBetween(Vector2.zero, player.transform.position);

        float distance = Vector2.Distance(Vector2.zero, player.transform.position); 

        Vector2 power = (Vector2.up * PushPower * distance).Rotate(angle);

        player.AddVelocity(power); 
    }

}


