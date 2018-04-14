using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Player;
using ChainedRam.Core.Projection;
using UnityEngine;

public class ShatterCollision : CollisionEffect<GemShatterer>
{
    public int ShardCount;
    public Projectile ShardPrefab; 


    public override void OnHit(GemShatterer g)
    {
        ShardPrefab.gameObject.SetActive(false); 
        for (int i = 0; i < ShardCount; i++)
        {
            var shard = Instantiate(ShardPrefab);
            shard.transform.position = transform.position; 
            shard.transform.eulerAngles = transform.eulerAngles + (Vector3.forward * (i * 360/ShardCount));
            shard.Setup(1);
            shard.gameObject.SetActive(true);
        }

        gameObject.SetActive(false); 
    }

}
