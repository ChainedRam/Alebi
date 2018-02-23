using System;
using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Collection;
using ChainedRam.Core.Player;
using UnityEngine;
using UnityEngine.UI;

public class StatusDisplayList : MonoBehaviour
{
	public Text text; 

	public void SetStatuses(List<KeyValue<IStatusEffect, float>> effects)
	{
		string build = "EffectsL\n";

		foreach(var pair in effects)
		{
			build += pair.Key.Name + ": " + pair.Value.ToString("0.00") + "\n";
		}

		text.text = build; 
	}
}
