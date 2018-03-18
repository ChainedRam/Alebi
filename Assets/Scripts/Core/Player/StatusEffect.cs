using ChainedRam.Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represent a status affect that when apply, the player get effected by it. 
/// Keep in mind that player is responsible of restoring it's states using <see cref="Player.RestorePlayerState"/>.
/// Player calls <see cref="Player.StorePlayerState"/> before applying any effect. 
/// </summary>
public abstract class StatusEffect : IStatusEffect
{
    /// <summary>
    /// Status effect name. 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Create an effect with a name. 
    /// </summary>
    /// <param name="name"></param>
    public StatusEffect(string name)
    {
        Name = name; 
    }

    public abstract void Init(Player p);

    /// <summary>
    /// Apply effect on player. 
    /// </summary>
    /// <param name="p"></param>
    public abstract void Apply(Player p);


    public abstract void Revert(Player p);
}
