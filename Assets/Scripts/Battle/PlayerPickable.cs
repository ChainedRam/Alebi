using ChainedRam.Core.Interface;
using ChainedRam.Core.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickable : MonoBehaviour, IPickable<Projectile>
{
    public Projectile ProjectilePrefab; 

    public object Source => reciever; 

    private object reciever; 

    public  void OnPicked(IReciever<Projectile> reciever)
    {
        this.reciever = reciever;

        WhenPicked(reciever);

        reciever.OnRecieve(ProjectilePrefab); 
    }

    protected virtual void WhenPicked(IReciever<Projectile> reciever)
    {
        gameObject.SetActive(false);
    }
}
