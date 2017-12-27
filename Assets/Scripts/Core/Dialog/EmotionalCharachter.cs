using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For emotionful dialogs. TODO: please change to an event listener 
/// </summary>
public class EmotionalCharachter : MonoBehaviour
{
    public Animator Animator;

    private void Start()
    {
        Animator.SetTrigger("idle");
    }

    public virtual void SetEmotion(string name)
    {
        //Hold if + at end 
        if (name.EndsWith("+"))
        {
            name = name.Substring(0, name.Length - 1);
            Animator.SetBool("hold", true);
        }
        else
        {
            Animator.SetBool("hold", false);
        }

        Animator.SetTrigger(name);
    }
}
