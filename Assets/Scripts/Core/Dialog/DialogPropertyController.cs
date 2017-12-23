using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows changing <see cref="Dialog.Property"/> in inspecter. Requires a DialogSegment Component
/// </summary>
public class DialogPropertyController : MonoBehaviour
{
    public DialogSegment Segment; 

    public bool Space;
    public bool NewLine;
    public bool PageEnd;
    public bool NewPage;

    void Update()
    {
        EnumExtensions.SetFlag(ref Segment.property, DialogPauseProperty.Space, Space );
        EnumExtensions.SetFlag(ref Segment.property, DialogPauseProperty.NewLine, NewLine);
        EnumExtensions.SetFlag(ref Segment.property, DialogPauseProperty.PageEnd, PageEnd);
        EnumExtensions.SetFlag(ref Segment.property, DialogPauseProperty.NewPage, NewPage);
    }

    private void OnValidate()
    {
        if(Segment == null)
        {
            Segment = GetComponent<DialogSegment>();

            if(Segment == null)
            {
                Debug.LogError("Missing DialogSegment component.", this); 
            }
        }
    }

}
