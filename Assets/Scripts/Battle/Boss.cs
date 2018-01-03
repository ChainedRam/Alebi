
using ChainedRam.Core;
/// <summary>
/// Boss when controls waves. 
/// </summary>
public class Boss : DamageReciever
    {
        /// <summary>
        /// Trigger to end and move to next boss phase 
        /// </summary>
        public TriggerSelector TriggerMeSelecter;

        /// <summary>
        /// Moves to next phase when recieving damage. 
        /// </summary>
        /// <param name="item"></param>
        public override void OnReceive(DamagePickable item)
        {
            base.OnReceive(item);

            TriggerMeSelecter.Next();
        }
    }
