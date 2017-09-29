using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    public Text text;

    public GameObject PauseIndecator; 

    public DialogSegment CurrentSegment;

    private float CurrentTime; 
    private bool IsPaused;
    private Action OnResume;  

    private void Start()
    {
        ResetTime(); 
        ResumeDialog(); 
    }


    private void Update()
    {
        if(Input.anyKeyDown)
        {
            ResumeDialog(); 
        }

        if (CurrentSegment == null || IsPaused)
        {
            return;
        }

        CurrentTime += Time.deltaTime;

        if (CurrentTime <= 0)
        {
            return; 
        }

        if (CurrentSegment.HasNext())
        {
            char next = CurrentSegment.NextCharachter();
            text.text += next;

            if (next == '\n')
            {
                if(CurrentSegment.property.HasFlag(DialogProperty.PauseOnNewLine))
                {
                    PauseDialog(); 
                }
                CurrentTime = -CurrentSegment.LineDisplayTime;
            }
            else 
            {
                if (next == ' ' && CurrentSegment.property.HasFlag(DialogProperty.PauseOnSpace))
                {
                    PauseDialog();
                }
                CurrentTime = -CurrentSegment.CharachterDisplayTime;
            }
        }
        else //finished dialog 
        {
            if (CurrentSegment.property.HasFlag(DialogProperty.PauseAtEnd))
            {
                PauseDialog();
                OnResume += ClearText;
                OnResume += () => OnResume -= ClearText;
            }
            else
            {
                ClearText();
            }

            CurrentSegment = null;
        }
    }

    public void PresentSegment(DialogSegment newSegment)
    {
        ClearText();
        CurrentSegment = newSegment;

        CurrentSegment.ResetDialog(); 
        ResumeDialog(); 
    }

    public void ResetTime()
    {
        CurrentTime = 0;
    }

    public void ClearText()
    {
        text.text = string.Empty; 
    }

    public void PauseDialog()
    {
        IsPaused = true;
        PauseIndecator.SetActive(true); 
    }

    public void ResumeDialog()
    {
        OnResume?.Invoke(); 
        IsPaused = false;
        PauseIndecator.SetActive(false);
    }
}
