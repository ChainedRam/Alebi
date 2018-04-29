using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ChainedRam.Core.Dialog
{
    public class TwoWayDialogBuilder : MonoBehaviour
    {
        [Header("Text Source")]
        public TextAsset TextFile;

        [Header("Conversation Settings")]
        public string ConversationName;

        [Header("Dialog Settings")]
        public DialogPauseType PauseProprty;

        [Range(0, 2)]
        public float DelayTime;

        [Header("Charachter Names")]
        public string FirstName;
        public string SecondName;

        [Header("Charachter Alias")]
        public string FirstAlias;
        public string SecondAlias;

        private Dictionary<string, string> AliasNameDic;
        private Dictionary<string, TextDialog> AliasPreafabDic;

        [Header("Output Conversation")]
        public Conversation Output;

        [Header("Required Refrences")]
        public DialogBox box;
        public Text ErrorMessage;

        public Text FirstSpeakerLabel;
        public Text SecondSpeakerLabel;

        [Header("Prefab")]
        public Conversation ConversationPrefab;

        public TextDialog FirstPrefabTemplate;
        public TextDialog SecondPrefabTemplate;

        private void Awake()
        {
            AliasNameDic = new Dictionary<string, string>() { { FirstAlias, FirstName }, { SecondAlias, SecondName } };
            AliasPreafabDic = new Dictionary<string, TextDialog>() { { FirstAlias, FirstPrefabTemplate }, { SecondAlias, SecondPrefabTemplate } }; //THANK YOU
        }

        private void Start()
        {
            OnValidate();

            FirstSpeakerLabel.text = FirstName;
            SecondSpeakerLabel.text = SecondName;

            string[] lines = TextFile.text.Split('\n');

            Output = Instantiate(ConversationPrefab);
            Output.name = string.IsNullOrWhiteSpace(ConversationName) ? "Unnamed Convo :]" : ConversationName;

            List<TextDialog> dialogs = new List<TextDialog>();

            foreach (string line in lines)
            {
                //lines to skip 
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("*"))
                {
                    continue;
                }

                if (line.Length > 2 && line[1] == ':')
                {
                    string alias = "" + line[0];

                    if (AliasNameDic.ContainsKey(alias) == false)
                    {
                        throw new System.Exception("Illegal Alias: '" + alias + "'. Must be one of two: '" + FirstAlias + "' or'" + SecondAlias + "'.");
                    }

                    TextDialog dialog = Instantiate(AliasPreafabDic[alias], Output.transform);
                    dialog.PauseProperty = this.PauseProprty;
                    dialog.Delay = this.DelayTime;

                    dialogs.Add(dialog);
                    dialogs.Last().name = dialogs.Count + " " + AliasNameDic[alias];

                    dialogs.Last().RawText = line.Substring(2).Trim();

                }
                else
                {
                    dialogs.Last().RawText += "\n" + line.Trim();
                }
            }

            Output.Dialogs = dialogs.ToArray();
        }

        public void PresentOutputUsing()
        {
            box.StartDialog(Output);
        }

        public void PlayDialogAtIndex(InputField input)
        {
            int index;

            string raw = input.text.Trim();

            if (int.TryParse(raw, out index) == false)
            {
                DisplayErrorMessage("Dummy! '" + raw + "' is not a number.");
                //Debug.LogError("Dummy! " + input.text + " is not a number.");
                return;
            }


            //from 1 to MAX
            if (index <= 0 || index > Output.Dialogs.Length)
            {
                DisplayErrorMessage("DUMB DUMB! Index must be between 1 & " + Output.Dialogs.Length + "!");
                // Debug.LogError("DUMB DUMB! Index must be between 1 & " + Output.Dialogs.Length + "!");
                return;
            }

            DialogIndex = index - 1;
            //from 0 to (MAX-1)
            box.StartDialog(Output.Dialogs[index - 1]);
        }

        int DialogIndex;
        public void StopCurrentDialog()
        {
            Output.WhenDialogEnd();

            if (DialogIndex > 0 && DialogIndex < Output.Dialogs.Length)
            {
                Output.Dialogs[DialogIndex].WhenDialogEnd();
            }

            box.EndDialog();
        }

        IEnumerator currentErrorMessage;
        void DisplayErrorMessage(string message)
        {
            if (currentErrorMessage != null)
            {
                StopCoroutine(currentErrorMessage);
            }

            currentErrorMessage = DisplayErrorCoroteen(message);

            StartCoroutine(currentErrorMessage);
        }


        IEnumerator DisplayErrorCoroteen(string message, float forseconds = 5f)
        {
            ErrorMessage.text = message;

            yield return new WaitForSeconds(forseconds);

            ErrorMessage.text = "";
        }

        //make sure Talal doesn't deviate 
        private void OnValidate()
        {
            #region TextFile
            if (TextFile == null)
            {
                Debug.LogError("TextFile cannot be null");
            }
            #endregion
            #region Prefab
            if (ConversationPrefab == null)
            {
                Debug.LogError("ConversationPrefab cannot be null");
            }
            if (FirstPrefabTemplate == null)
            {
                Debug.LogError("FirstPrefabTemplate cannot be null");
            }

            if (SecondPrefabTemplate == null)
            {
                Debug.LogError("SecondPrefabTemplate cannot be null");
            }
            #endregion
            #region Names
            if (string.IsNullOrEmpty(FirstName))
            {
                Debug.LogError("FirstName cannot be empty");
            }

            if (string.IsNullOrEmpty(SecondName))
            {
                Debug.LogError("SecondName cannot be empty");
            }
            #endregion
            #region Alias
            if (string.IsNullOrEmpty(FirstAlias))
            {
                Debug.LogError("FirstAlias cannot be empty");
            }
            else if (FirstAlias.Length != 1)
            {
                Debug.LogError("FirstAlias must be 1 charachter(letter only)");
            }

            if (string.IsNullOrEmpty(SecondAlias))
            {
                Debug.LogError("SecondAlias cannot be empty");
            }
            else if (SecondAlias.Length != 1)
            {
                Debug.LogError("FirstAlias must be 1 charachter(letter only)");
            }
            #endregion Alias
            #region Text Labels
            if (FirstSpeakerLabel == null)
            {
                Debug.LogError("FirstSpeakerLabel must be refrenced");
            }
            if (SecondSpeakerLabel == null)
            {
                Debug.LogError("SecondSpeakerLabel must be refrenced");
            }
            #endregion
        }
    }
}