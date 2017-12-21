using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class DialogPropertyExtentions
{
   
    public static bool HasFlag(this DialogPauseProperty property, DialogPauseProperty flag)
    {
        return (property & flag) == flag; 
    }

    public static void AddFlag(this DialogPauseProperty property, DialogPauseProperty flag)
    {
        property = property | flag; 
    }

    public static void RemoveFlag(this DialogPauseProperty property, DialogPauseProperty flag)
    {
        property  =  property & ~flag;
    }
}
