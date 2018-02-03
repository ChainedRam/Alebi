using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Caster<A> : MonoBehaviour
{
	public abstract void RequestCast(A action);
}

public abstract class Caster : Caster<string> 
{
}
