using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Implements helper functions such as "Freeze" and "Release" for X Y axes for a Ridgidbody2D. 
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class AxesFreezer : MonoBehaviour
{
    #region Inspecter Variables
    /// <summary>
    /// Rigidbody2D to apply axes freezinf. 
    /// </summary>
    public Rigidbody2D target;

    private void Start()
    {
        target = GetComponent<Rigidbody2D>();
        if (target == null)
        {
            Debug.LogError("Rigidbody2D is null in " + name + ".", this);
        }
    }
    #endregion
    #region Public Methods
    /// <summary>
    /// Added freeze constaint on X position. 
    /// </summary>
    public void FreazeVertical()
    {
        target.constraints = target.constraints | RigidbodyConstraints2D.FreezePositionX;
    }

    /// <summary>
    /// Added freeze constaint on Y position. 
    /// </summary>
    public void FreazeHorizontal()
    {
        target.constraints = target.constraints | RigidbodyConstraints2D.FreezePositionY;
    }

    /// <summary>
    /// Removes freeze constaint on X position. 
    /// </summary>
    public void ReleaseVertical()
    {
        target.constraints = target.constraints & ~RigidbodyConstraints2D.FreezePositionX;
    }

    /// <summary>
    /// Removes freeze constaint on Y position. 
    /// </summary>
    public void ReleaseHorizontal()
    {
        target.constraints = target.constraints & ~RigidbodyConstraints2D.FreezePositionY;
    }
    #endregion
    #region validatation
    private void OnValidate()
    {
        if (target == null)
        {
            target = GetComponent<Rigidbody2D>();

            if (target == null)
            {
                Debug.LogWarning("Ridgebody target is null in " + name + ".", this);
            }
        }
    }
    #endregion

}

