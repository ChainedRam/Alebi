using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    public Text text;

    public GameObject PauseIndecator; 

    public Dialog CurrentDialog;

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

        if (CurrentDialog == null || IsPaused)
        {
            return;
        }

        CurrentTime += Time.deltaTime;

        if (CurrentTime <= 0)
        {
            return; 
        }

        if (CurrentDialog.HasNext())
        {
            char next = CurrentDialog.NextCharachter();
            text.text += next;

            if (next == '\n')
            {
                if (CurrentDialog.Property.HasFlag(DialogPauseProperty.NewLine))
                {
                    PauseDialog();
                }
                else
                {
                    CurrentTime = -CurrentDialog.GetDisplayDelay();
                }
            }
            else 
            {
                if (next == ' ' && CurrentDialog.Property.HasFlag(DialogPauseProperty.Space))
                {
                    PauseDialog();
                }
                else if (CurrentDialog.Property.HasFlag(DialogPauseProperty.NewPage) && next == '\0')
                {
                    OnResume += ClearText;
                    OnResume += () => OnResume -= ClearText;

                    PauseDialog();
                }
                else
                {
                    CurrentTime = -CurrentDialog.GetDisplayDelay();
                }
            }
        }
        else //finished dialog 
        {
            if (CurrentDialog.Property.HasFlag(DialogPauseProperty.PageEnd))
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
        CurrentDialog?.WhenDialogEnd();
        CurrentDialog = null;
    }

    public void PresentDialog(Dialog dialog)
    {
        if(CurrentDialog != null)
        {
            CurrentDialog.WhenDialogEnd();
        }


        ClearText();
        CurrentDialog = dialog;

        CurrentDialog.ResetDialog();
        CurrentDialog.WhenDialogStart();

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
        CurrentDialog.WhenDialogPause();
        PauseIndecator.SetActive(true); 
    }

    public void ResumeDialog()
    {
        CurrentDialog.WhenDialogResume();

        OnResume?.Invoke();
        IsPaused = false;
        PauseIndecator.SetActive(false);
    }

    public void ForceEndDialog()
    {
        EndCurrentDialogSegment(); 

        IsPaused = false;
        PauseIndecator.SetActive(false);
    }

    protected bool PageIsFull()
    {
        return false; //TODO maybe someday 
    }
}
