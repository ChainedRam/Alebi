using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Dialog
{
    /// <summary>
    /// A component that display dialogs. 
    /// </summary>
    public abstract class DialogDisplayer : MonoBehaviour
    {
        #region Private Fields 
        private Dialog CurrentDialog;
        private IEnumerator<char> CharactersEnumertor;
        private float CurrentWaitTime;
        private bool IsPaused;
        private Action OnResumeAction;
        #endregion
        #region Unity Methods
        private void Start()
        {
            ClearDisplay();
        }

        private void Update()
        {
            UpdatePauseStatus(); 

            if(CurrentDialog == null || IsPaused)
            {
                return;
            }

            CurrentWaitTime += Time.deltaTime;
            if (CurrentWaitTime < 0)
                return;

            if (CharactersEnumertor.MoveNext())
            {
                RecieveCharacter(CharactersEnumertor.Current);
                ApplyDialogDelay();
            }
            else
            {
                DialogReachedEnd();
            }

        }

        private void UpdatePauseStatus()
        {
            if (IsPaused && ShouldResume())
            {
                Resume();
            }
        }
        #endregion
        #region Private Methods
        private void RecieveCharacter(char nextLetter)
        {
            switch (nextLetter)
            {
                case '|':
                    Pause();
                    break;
                case '_':
                    ClearText();
                    break;
                default:
                    AppendLetter(nextLetter);
                    break;
            }
        }
        private void DialogReachedEnd()
        {
            if (ShouldPause(CurrentDialog.Property))
            {
                Pause();

                OnResumeAction += EndCurrentDialog;
                OnResumeAction += () => OnResumeAction -= EndCurrentDialog;
            }
            else
            {
                EndCurrentDialog();
            }
        }
        private void ApplyDialogDelay()
        {
            CurrentWaitTime = -CurrentDialog.GetDisplayDelay();
        }
        private void CheckPause(DialogPauseType flag)
        {
            if (flag != DialogPauseType.None && ShouldPause(flag))
            {
                PauseDialog();
            }
        }
        private bool ShouldPause(DialogPauseType flag)
        {
            return CurrentDialog.Property.HasFlag(DialogPauseType.End);
        }

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
            OnPause(); 
        }
        private void Resume()
        {
            IsPaused = false;
            OnResumeAction?.Invoke(); 
            OnResume(); 
        }
        #endregion
        #region Protected Virtual Methods
        protected virtual void OnPause() { }
        protected virtual void OnResume() { }
        protected virtual bool ShouldResume()
        {
            return Input.anyKey;
        }
        #endregion
        #region Protected Abstract Methods
        protected abstract void ClearText();
        protected abstract void AppendLetter(char next);
        #endregion
        #region Public Methods
        public void StartDialog(Dialog dialog)
        {
            gameObject.SetActive(true);
            if (CurrentDialog != null)
            {
                CurrentDialog.WhenDialogEnd();
            }
            CurrentDialog = dialog;
            CurrentDialog.ResetDialog();
            CurrentDialog.WhenDialogStart();

            CharactersEnumertor = CurrentDialog.Characters();
            ClearDisplay();
        }

        public void PauseDialog()
        {
            Pause();
            CurrentDialog.WhenDialogPause();
        }

        public void ResumeDialog()
        {
            CurrentDialog.WhenDialogResume();

            OnResumeAction?.Invoke();
            Resume();
        }

        public void EndDialog()
        {
            EndCurrentDialog();
        }

        public void ClearDisplay()
        {
            IsPaused = false;
            CurrentWaitTime = 0;
            ClearText();
        } 
        #endregion
    }
}
