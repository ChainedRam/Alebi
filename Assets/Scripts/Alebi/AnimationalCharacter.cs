using ChainedRam.Core.Dialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationalCharacter : EmotionalCharacter
{
    public GameObject Shade;
    public Animator animator;
    public AnimationClip[] clips;

    private AnimatorOverrideController OverrideController;
    private string EmotionHoldName = "PlaceHolder";
    private Dictionary<string, AnimationClip> ClipsDictionary;

    private string currentStateName;

    private void Start()
    {
        currentStateName = "";
        OverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = OverrideController;

        ClipsDictionary = new Dictionary<string, AnimationClip>();

        foreach (AnimationClip clip in clips)
        {
            string emotionName = clip.name.Substring(clip.name.LastIndexOf('_') + 1);
            ClipsDictionary.Add(emotionName.ToLower(), clip);
        }
    }

    public override void SetEmotion(string emotionName)
    {
        Release();

        if (emotionName.EndsWith("+"))
        {
            emotionName = emotionName.Substring(0, emotionName.Length - 1);
            Hold();
        }

        currentStateName = emotionName;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("default") == false)
        {
            animator.SetTrigger("idle");
        }

    }


    //because state machine can't do shit
    private void LateUpdate()
    {
        if (animator == null)
        {
            return;
        }
        if (currentStateName != "" && animator.GetCurrentAnimatorStateInfo(0).IsName("default"))
        {
            if (ClipsDictionary.ContainsKey(currentStateName))
            {
                OverrideController[EmotionHoldName] = ClipsDictionary[currentStateName];
                animator.runtimeAnimatorController = OverrideController;
                animator.SetTrigger("emotion");
                currentStateName = "";
            }
            else
            {
                Debug.LogError("Missing emotion: " + currentStateName);
            }
        }
    }

    /// <summary>
    /// Release animation Hold 
    /// </summary>
    public void Release()
    {
        animator.SetBool("hold", false);
    }

    /// <summary>
    /// Hold animation Hold 
    /// </summary>
    public void Hold()
    {
        animator.SetBool("hold", true);
    }

    public override void Darken()
    {
        Shade.SetActive(true);
    }

    public override void Undarken()
    {
        Shade.SetActive(false);
    }

    private void OnDestroy()
    {
        if (OverrideController != null)
        {
            Destroy(OverrideController);
        }
    }
}
