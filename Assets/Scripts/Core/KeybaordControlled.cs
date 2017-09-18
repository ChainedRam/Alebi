using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows Object to  move using keybaord.
/// </summary>
public class KeybaordControlled : MonoBehaviour
{
    public float Speed;

    //my body is ready, do not inheret
    new private Rigidbody2D rigidbody2D;

   
    private Dictionary<string, Vector2> RigesteredKeys; //can improve to be displayable in inspecter.  

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        RigesteredKeys = new Dictionary<string, Vector2>()
        {
            { "up", Vector2.up },
            { "down", Vector2.down },
            { "right", Vector2.right },
            { "left", Vector2.left }
        }; 
    }
    /// <summary>
    /// Move up or left right 
    /// </summary>
	void FixedUpdate ()
    {
        foreach (var key in RigesteredKeys)
        {
            if (Input.GetKey(key.Key))
            {
                rigidbody2D.velocity += (key.Value * Speed * Time.deltaTime);
            }
        }

    }
}
