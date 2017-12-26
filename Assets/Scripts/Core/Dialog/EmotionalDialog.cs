using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Parses text to extra emotion that will trigger at their mentioned time. 
/// </summary>
public class EmotionalDialog : TextDialog
{
    public EmotionalCharachter Charachter;

    private List<EmotionalIndex> EmotionStack;
    private int EmotionIndex;

    public UnityEvent OnStart;
    public UnityEvent OnEnd;


    /// <summary>
    /// Parses text on run time.  //Think of a better way. No need to recompute this 
    /// </summary>
    void Start()
    {
        EmotionStack = new List<EmotionalIndex>();
        EmotionIndex = 0; 
        //parse text 
        bool start = false;

        string result = "";
        string emotionBuild = "";

        for (int i= 0; i < Text.Length; i++)
        {
            char c = Text[i];

            switch (c)
            { 
                case '<':
                    emotionBuild = "";
                    start = true;
                    break;
                case '>':
                    EmotionStack.Add(new EmotionalIndex(result.Length, emotionBuild));
                    start = false;
                    break;
                default:
                    if (start)
                    {
                        emotionBuild += "" + c;
                    }
                    else
                    {
                        result += "" + c;
                    }
                    break; 
            }
        }

        Text = result;
    }

    /// <summary>
    /// Trigger emotion when time is ready 
    /// </summary>
    /// <returns></returns>
    public override char NextCharachter()
    {
        if(EmotionIndex < EmotionStack.Count && Index == EmotionStack[EmotionIndex].Index)
        {
            Charachter.SetEmotion(EmotionStack[EmotionIndex].Emotion);
            EmotionIndex++; 
        }

        return base.NextCharachter();
    }

    public override void WhenDialogResume()
    {
        base.WhenDialogResume();
    }

    public override void ResetDialog()
    {
        base.ResetDialog();
        EmotionIndex = 0; 
    }

    public override void WhenDialogStart()
    {
        base.WhenDialogStart();
        EmotionIndex = 0;

        OnStart?.Invoke();
    }

    public override void WhenDialogEnd()
    {
        base.WhenDialogEnd();
        OnEnd?.Invoke();
    }

    /// <summary>
    /// Extratable to an external component to better manage parsed emotions. //TODO 
    /// </summary>
    private class EmotionalIndex
    {
        public int Index;
        public string Emotion;

        public EmotionalIndex(int i, string e)
        {
            Index = i;
            Emotion = e; 
        }
    }
}

