using System;
using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Collection;
using ChainedRam.Core.Player;
using UnityEngine;
using UnityEngine.UI;

public class StatusDisplayList : MonoBehaviour
{
    public Text StatusText;
    public Text HealthText; 

    public void SetStatuses(List<KeyValue<IStatusEffect, float>> effects)
    {
        string build = "Effects\n";

        foreach(var pair in effects)
        {
            build += pair.Key.Name + ": " + pair.Value.ToString("0.00") + "\n";
        }

        if (StatusText != null)
        {
            StatusText.text = build; 
        }
        else
        {
            Debug.LogWarning("Missing text reference");
        }
    }

    public void SetHealth(int health)
    {
        HealthText.text = "Health: " + health; 
    }
}
