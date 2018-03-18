
using ChainedRam.Core;
using ChainedRam.Core.Selection;
/// <summary>
/// Boss when controls waves. 
/// </summary>
public class Boss : DamageReciever
{

    /// <summary>
    /// Moves to next phase when recieving damage. 
    /// </summary>
    /// <param name="item"></param>
    public override void OnReceive(DamagePickable item)
    {
        base.OnReceive(item);
        
    }
}
