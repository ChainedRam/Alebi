using ChainedRam.Core.Collider;
using UnityEngine;


/// <summary>
/// A Pickable Damage component. 
/// </summary>
public class DamagePickable : Pickable
{
    /// <summary>
    /// Holds damage value 
    /// </summary>
    public int Value;

    public override bool CanBePicked(GameObject PickedBy)
    {
        return true;
    }

    public override void OnPicked(GameObject PickedBy)
    {

    }
}
