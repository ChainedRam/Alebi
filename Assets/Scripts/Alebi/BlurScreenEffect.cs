using ChainedRam.Core.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class BlurScreenEffect : MonoBehaviour, IStatusEffect
{
    public BlurOptimized blur;
    public void Show()
	{
        blur.downsample = 1;
        blur.blurIterations = 2;
        blur.blurSize = 3;
		gameObject.SetActive(true);
	}

	public void Hide()
	{
        blur.downsample = 0;
        blur.blurIterations = 1;
        blur.blurSize = 0;
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
