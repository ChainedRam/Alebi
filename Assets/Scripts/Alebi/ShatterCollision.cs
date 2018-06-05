using ChainedRam.Core.Projection;
using UnityEngine;

public class ShatterCollision : CollisionEffect<GemShatterer>
{
    public int ShardCount;
    public Projectile ShardPrefab;

    private Projectile[] Shards;

    private void Start()
    {
        Shards = new Projectile[ShardCount]; 
    }

    public override void OnHit(GemShatterer g)
    {
        ShatterCmd();
    }

    public void ShatterCmd()
    {
        for (int i = 0; i < Shards.Length; i++)
        {
            if (Shards[i] == null)
            {
                Shards[i] = Instantiate(ShardPrefab);
            }
            Shards[i].transform.position = transform.position;
            Shards[i].transform.localEulerAngles = (Vector3.forward * (i * 360f / Shards.Length));
            Shards[i].Setup(1);
            Shards[i].gameObject.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
