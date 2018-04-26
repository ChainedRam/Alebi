using ChainedRam.Core.Dialog.Extention;
using ChainedRam.Core.Enums.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChainedRam.Core.Dialog
{
    public class DialogBox : MonoBehaviour
    {
        #region Public Attributes 
        public Text TextBox;
        public GameObject PauseIndecator; //TODO get by finding + set to private

        #endregion
        #region Private Attributes
        public Dialog CurrentDialog;
        private float CurrentTime;
        private bool IsPaused;
        private Action OnResume; 
        #endregion

        #region Unity
        private void Start()
        {
            ResetTime();
            ResetPause();
        }

        private void Update()
        {
            ListenForResumeInput();

            if (IsDialogReady() == false)
            {
                return;
            }

            CurrentTime += Time.deltaTime;
            if (IsDelayOnCooldown())
            {
                return;
            }

            if (CurrentDialog.HasNext())
            {
                RecieveCharacter(CurrentDialog.NextCharachter());
                ApplyDialogDelay();
            }
            else
            {
                DialogReachedEnd();
            }

        } //end update 
        #endregion 
        #region Called From Update
        private void ListenForResumeInput()
        {
            if (Input.anyKeyDown && IsPaused)
            {
                ResumeDialog();
            }
        }

        private bool IsDialogReady()
        {
            return CurrentDialog != null && !IsPaused;
        }

        private bool IsDelayOnCooldown()
        {
            return CurrentTime < 0;
        }

        private void RecieveCharacter(char next)
        {
            AppendToDisplay(next);

            if (IsForcedPauseCharachter(next))
            {
                ClearTextOnResume();
            }

            CheckPause(next.ToPauseProperty());
        }

        private void DialogReachedEnd()
        {
            if (ShouldPause(CurrentDialog.Property))
            {
                Pause();

                OnResume += EndCurrentDialog;
                OnResume += () => OnResume -= EndCurrentDialog; 
            }
            else
            {
                EndCurrentDialog();
            }
        }
        #endregion
        #region RecieveCharacter Helpers
        private void AppendToDisplay(char next)
        {
            TextBox.text += next;
        }

        private bool IsForcedPauseCharachter(char next)
        {
            return next == '\0';
        }

        private void ApplyDialogDelay()
        {
            CurrentTime = -CurrentDialog.GetDisplayDelay();
        }

        private void ClearTextOnResume()
        {
            OnResume += ClearText;
            OnResume += () => OnResume -= ClearText;
        }

        private void CheckPause(DialogPauseType flag)
        {
            if(flag != DialogPauseType.None && ShouldPause(flag))
            {
                PauseDialog(); 
            }
        }
        private bool ShouldPause(DialogPauseType flag)
        {
            return CurrentDialog.Property.HasFlag(flag); 
        }
        #endregion
        #region RecieveCharacter helpers
        private void EndCurrentDialog()
        {
            ClearText();
            CurrentDialog?.WhenDialogEnd();
            CurrentDialog = null;

            gameObject.SetActive(false);

        }

        private void Pause()
        {
            IsPaused = true;
            PauseIndecator.SetActive(true);
        }

        private void ResetPause()
        {
            IsPaused = false;
            PauseIndecator.SetActive(false);
        }
        #endregion
        #region Public Methods
        public void PresentDialog(Dialog dialog)
        {
            gameObject.SetActive(true); 
            if (CurrentDialog != null)
            {
                CurrentDialog.WhenDialogEnd();
            }


            ClearText();
            CurrentDialog = dialog;

            CurrentDialog.ResetDialog();
            CurrentDialog.WhenDialogStart();

            ResetPause();
        }

        public void ResetTime()
        {
            CurrentTime = 0;
        }

        public void ClearText()
        {
            TextBox.text = string.Empty;
        }

        public void PauseDialog()
        {
            Pause();
            CurrentDialog.WhenDialogPause();
        }

        public void ResumeDialog()
        {
            CurrentDialog.WhenDialogResume();

            OnResume?.Invoke();
            ResetPause();
        }

        public void ForceEndDialog()
        {
            EndCurrentDialog();
            ResetPause();
        }
        #endregion
    }
}
