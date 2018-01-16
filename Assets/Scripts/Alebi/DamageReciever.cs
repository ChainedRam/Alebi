

using ChainedRam.Core.Collider;

/// <summary>
/// Allows objects to recieve Damage 
/// </summary>
public class DamageReciever : Collider2DReciever<DamagePickable>
{
    //Used for demonstration. TODO remove 
    public int HP;

    public override void OnReceive(DamagePickable item)
    {
        HP -= item.Value;
    }
}

