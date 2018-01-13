using ChainedRam.Core.Enums.Extensions;
using UnityEngine;

namespace ChainedRam.Core.Dialog
{
    /// <summary>
    /// Allows changing <see cref="Dialog.Property"/> in inspecter. Requires a DialogSegment Component
    /// </summary>
    public class DialogPropertyController : MonoBehaviour
    {
        public DialogSegment Segment;

        public bool Space;
        public bool NewLine;
        public bool PageEnd;
        public bool NewPage;

        void Update()
        {
            EnumExtensions.SetFlag(ref Segment.property, DialogPauseType.Space, Space); //TODO ref code smell 
            EnumExtensions.SetFlag(ref Segment.property, DialogPauseType.NewLine, NewLine);
            EnumExtensions.SetFlag(ref Segment.property, DialogPauseType.End, PageEnd);
            EnumExtensions.SetFlag(ref Segment.property, DialogPauseType.NewPage, NewPage);
        }

        private void OnValidate()
        {
            if (Segment == null)
            {
                Segment = GetComponent<DialogSegment>();

                if (Segment == null)
                {
                    Debug.LogError("Missing DialogSegment component.", this);
                }
            }
        }
    }
}
