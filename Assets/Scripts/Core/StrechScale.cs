using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrechScale : MonoBehaviour
{
	public Vector3 StrechTo;

	public float Speed;

	private void Awake()
	{
		enabled = true; 
	}

	public void BeginStreching()
	{
		enabled = true; 
	}

	private void Update()
	{
		transform.localScale = Vector3.MoveTowards(transform.localScale, StrechTo, Speed); 
	}
}
