using ChainedRam.Core.Dialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Dialog.Extention
{ 
    public static class PausePropertyCharExtention
    {
        public static DialogPauseType ToPauseProperty(this char type)
        {
            switch (type)
            {
                case '\0':
                    return DialogPauseType.End;
                case ' ':
                    return DialogPauseType.Space;
                case '\n':
                    return DialogPauseType.NewLine;
                default:
                    return DialogPauseType.None;

            }

            //throw new FailedConversionException($"Failed to convert '{type}' into a DialogPauseType");
        }
    }
}
