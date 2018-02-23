using ChainedRam.Core.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlurScreenEffect : MonoBehaviour, IStatusEffect
{
	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false); 
	}

    #region  IStatusEffect Interface Property
    public string Name
	{
		get
		{
			return "Blur";
		}
	}
    #endregion
    #region  IStatusEffect Interface Methods
    public void Init(Player p)
	{
		Show();
	}

	public void Apply(Player p)
	{

	}

	public void Revert(Player p)
	{
		Hide();
	} 
	#endregion
}
