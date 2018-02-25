using ChainedRam.Core.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class BlurScreenEffect : MonoBehaviour, IStatusEffect
{


    private BlurOptimized blur;

    private void Start()
    {
        blur = Camera.main.GetComponent<BlurOptimized>();
        if (blur == null)
        {
            blur = Camera.main.gameObject.AddComponent<BlurOptimized>();
            blur.downsample = 1;
            blur.blurIterations = 2;
            blur.blurSize = 3;
            blur.enabled = false;
        }
    }
    public void Show()
	{
        blur.enabled = true;
		gameObject.SetActive(true);
	}

	public void Hide()
	{
        blur.enabled = false;
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
