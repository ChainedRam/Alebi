
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ChainedRam.Core.Dialog
{
    /// <summary>
    /// Respresent a list of letters that share a common style.
    /// </summary>
    public class DialogFont : MonoBehaviour
    {
        #region Public Attributes 
        public List<Letter> Letters; 
        #endregion
        #region Private Attributes 

        [SerializeField, HideInInspector]
        private Dictionary<LetterType, Letter> LetterDictionary;
        #endregion
        #region Indexer
        /// <summary>
        /// Returns registered letter 
        /// </summary>
        /// <param name="letterType"></param>
        /// <returns></returns>
        public Letter this[LetterType type]
        {
            get
            {
                return GetLetter(type); 
            }
        }

        /// <summary>
        /// Returns registered letter 
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
        public Letter this[char index]
        {
            get
            {
                return GetLetter(index); 
            }
        }
        #endregion
        #region Unity Methods
        private void Start()
        {
            LetterDictionary = new Dictionary<LetterType, Letter>();
            PopularDictionary(LetterDictionary);
        } 
        #endregion
        #region Public Methods
        /// <summary>
        /// Returns registered letter 
        /// </summary>
        /// <param name="letterType"></param>
        /// <returns></returns>
        public Letter GetLetter(LetterType letterType)
        {
            if(LetterDictionary == null)
            {
                LetterDictionary = new Dictionary<LetterType, Letter>();
                PopularDictionary(LetterDictionary); 
            }

            if (LetterDictionary.ContainsKey(letterType))
            {
                return LetterDictionary[letterType];
            }

            throw new ArgumentException("LetterType " + letterType.ToString() + " has not been registered.");
        }

        /// <summary>
        /// Returns registered letter 
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
        public Letter GetLetter(char letter)
        {
            return GetLetter((LetterType)letter);
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// Builds letter dictionary from letter list 
        /// </summary>
        /// <param name="LetterDictionary"></param>
        private void PopularDictionary(Dictionary<LetterType, Letter> LetterDictionary)
        {
            foreach (var l in Letters)
            {
                try
                {
                    LetterDictionary.Add(l.Type, l);
                }
                catch (ArgumentException)
                {
                    Debug.LogErrorFormat("Duplicate letter type in '{0}' and '{1}'.", LetterDictionary[l.Type].name, l.name);
                }
            }
        } 
        #endregion
    }
}