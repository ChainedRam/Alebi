using System;

namespace ChainedRam.Core.Collider
{
    /// <summary>
    /// Recieves dynamic object to set the target pickable type 
    /// </summary>
    public class DynamicReciever : Collider2DReciever<Pickable>
    {
        public Pickable PickableType;

        public override void OnReceive(Pickable item)
        {

        }

        protected override Type GetTargetPickableType()
        {
            return PickableType.GetType();
        }

    }
}
