using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Dialog
{
    public enum LetterType
    {
        #region Numbers
        Zero = '0',
        One = '1',
        Two = '2',
        Three = '3',
        Four = '4',
        Five = '5',
        Six = '6',
        Seven = '7',
        Eight = '8',
        Nine = '9',
        #endregion
        #region Upper case letters
        A = 'A',
        B = 'B',
        C = 'C',
        D = 'D',
        E = 'E',
        F = 'F',
        G = 'G',
        H = 'H',
        I = 'I',
        J = 'J',
        K = 'K',
        L = 'L',
        M = 'M',
        N = 'N',
        O = 'O',
        P = 'P',
        Q = 'Q',
        R = 'R',
        S = 'S',
        T = 'T',
        U = 'U',
        V = 'V',
        W = 'W',
        X = 'X',
        Y = 'Y',
        Z = 'Z',
        #endregion
        #region Lower case letters
        a = 'a',
        b = 'b',
        c = 'c',
        d = 'd',
        e = 'e',
        f = 'f',
        g = 'g',
        h = 'h',
        i = 'i',
        j = 'j',
        k = 'k',
        l = 'l',
        m = 'm',
        n = 'n',
        o = 'o',
        p = 'p',
        q = 'q',
        r = 'r',
        s = 's',
        t = 't',
        u = 'u',
        v = 'v',
        w = 'w',
        x = 'x',
        y = 'y',
        #endregion
        #region Special characters 
        //Based on https://www.englishforums.com/content/resources/did-you-know-all-the-computer-keyboards-names-in-english.htm

        Space = ' ',
        /// <summary>
        /// ?
        /// </summary>
        QuestionMark = '?',

        /// <summary>
        /// !
        /// </summary>
        ExclamationMark = '!',

        /// <summary>
        /// :
        /// </summary>
        Colon = ':',

        /// <summary>
        /// ;
        /// </summary>
        Semicolon = ';',

        /// <summary>
        /// -
        /// </summary>
        Dash = '-',

        /// <summary>
        /// _
        /// </summary>
        Underscore = '_',

        /// <summary>
        /// (
        /// </summary>
        OpenParenthe = '(',

        /// <summary>
        /// )
        /// </summary>
        CloseParenthe = ')',

        /// <summary>
        /// +
        /// </summary>
        Plus = '+',

        /// <summary>
        /// -
        /// </summary>
        Minus = '-',

        /// <summary>
        /// .
        /// </summary>
        Period = '.',

        /// <summary>
        /// ,
        /// </summary>
        Comma = ',',

        /// <summary>
        /// #
        /// </summary>
        Pound = '#',

        /// <summary>
        /// %
        /// </summary>
        Percent = '%',

        /// <summary>
        /// '
        /// </summary>
        SingleQuotation = '\'',
        #endregion
        #region Functional Characters 
        Pause = -1,
        Clear = -2,
        #endregion
    } 
}
