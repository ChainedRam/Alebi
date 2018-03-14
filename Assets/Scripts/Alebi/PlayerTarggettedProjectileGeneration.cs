using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainedRam.Core.Projection;

public class PlayerTarggettedProjectileGeneration : TargettedProjectileGeneration

{


	[ContextMenu("Copy From Parent")]
	private void CopyFromTarget()
	{
		TargettedProjectileGeneration t = GetComponent<TargettedProjectileGeneration>();

		GenerateAt = t.GenerateAt;
		Target = t.Target;
		WaitTime = t.WaitTime;
		this.Prefab = t.Prefab;
		Parent = t.Parent;
		MotionOverride = t.MotionOverride; 
	}
}
