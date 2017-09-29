using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class DialogPropertyExtentions
{
   
    public static bool HasFlag(this DialogProperty property, DialogProperty flag)
    {
        return (property & flag) == flag; 
    }

    public static void AddFlag(this DialogProperty property, DialogProperty flag)
    {
        property = property | flag; 
    }

    public static void RemoveFlag(this DialogProperty property, DialogProperty flag)
    {
        property  =  property & ~flag;
    }
}
