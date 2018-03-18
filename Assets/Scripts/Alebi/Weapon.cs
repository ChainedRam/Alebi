using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : PlayerPickable
{
    public Projectile ProjectilePrefab;

    /// <summary>
    /// Fire projectile when picked 
    /// </summary>
    /// <param name="pickedBy"></param>
    public override void OnPicked(GameObject pickedBy)
    {
       Projectile proj = Instantiate(ProjectilePrefab, pickedBy.transform.position, pickedBy.transform.rotation, pickedBy.transform);
       proj.gameObject.SetActive(true); 
       //proj.Setup(); TODO 

       gameObject.SetActive(false);

    }
}
