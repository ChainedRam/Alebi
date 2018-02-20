using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Player;
using UnityEngine;
using UnityEngine.UI;

public class SlowAreaEffect : AreaEffect
{
    public float Duration;

    [Range(0f,1f)]
    public float SlowRate; 

    StatusEffect SlowEffect; 

    [Header("Optional for display")]
    public Text DurationLabel;


    private void Start()
    {
        SlowEffect = new SlowStatusEffect(Duration, 1- SlowRate);
    }

    public override void AddEffect(Player player)
    {
        player.AddEffect(SlowEffect, Duration);
    }

    public override void RefreshEffect(Player player)
    {
        player.AddEffect(SlowEffect, Duration);
    }

    public override void RemoveEffect(Player player)
    {

    }

    private void Update()
    {
         if(DurationLabel != null)
        {
            //DurationLabel.text = SlowEffect.Duration.ToString("0.00");
        }
    }
}
