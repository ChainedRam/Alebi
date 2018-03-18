using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public abstract string Name { get; protected set; } 
    public abstract void Cast(GameObject by);
}

