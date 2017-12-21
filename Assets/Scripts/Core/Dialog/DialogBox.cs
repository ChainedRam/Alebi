using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    public Text text;

    public GameObject PauseIndecator; 

    public Dialog CurrentSegment;

    private float CurrentTime; 
    private bool IsPaused;
    private Action OnResume;  

    private void Start()
    {
        ResetTime();
        PauseIndecator.SetActive(false);
        IsPaused = false;
    }

    private void Update()
    {
        //make this readable damn it. 
        if(Input.anyKeyDown && IsPaused)
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
                if (CurrentSegment.Property.HasFlag(DialogPauseProperty.NewLine))
                {
                    PauseDialog();
                }
                else
                {
                    CurrentTime = -CurrentSegment.GetDisplayDelay();
                }
            }
            else 
            {
                if (next == ' ' && CurrentSegment.Property.HasFlag(DialogPauseProperty.Space))
                {
                    PauseDialog();
                }
                else if (CurrentSegment.Property.HasFlag(DialogPauseProperty.NewPage) && next == '\0')
                {
                    OnResume += ClearText;
                    OnResume += () => OnResume -= ClearText;

                    PauseDialog();
                }
                else
                {
                    CurrentTime = -CurrentSegment.GetDisplayDelay();
                }
            }
        }
        else //finished dialog 
        {
            if (CurrentSegment.Property.HasFlag(DialogPauseProperty.PageEnd))
            {
                PauseDialog();
                OnResume += EndCurrentDialogSegment;
                OnResume += () => OnResume -= EndCurrentDialogSegment;
            }
            else
            {
                EndCurrentDialogSegment(); 
            }
        }
    }

    private void EndCurrentDialogSegment()
    {
        ClearText();
        CurrentSegment.WhenDialogEnd();
        CurrentSegment = null;
    }

    public void PresentSegment(Dialog newSegment)
    {
        ClearText();
        CurrentSegment = newSegment;

        CurrentSegment.ResetDialog();
        CurrentSegment.WhenDialogStart();

        IsPaused = false;
        PauseIndecator.SetActive(false);
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
        CurrentSegment.WhenDialogPause();
        PauseIndecator.SetActive(true); 
    }

    public void ResumeDialog()
    {
        CurrentSegment.WhenDialogResume();

        OnResume?.Invoke();
        IsPaused = false;
        PauseIndecator.SetActive(false);
    }


    public bool PageIsFull()
    {
        return false; //TODO maybe someday 
    }
}
