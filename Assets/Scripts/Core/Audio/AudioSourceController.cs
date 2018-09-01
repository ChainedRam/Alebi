using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AudioSourceController : MonoBehaviour {

    /// <summary>
    /// do not use 
    /// </summary>
    private static AudioSourceController _instance; 

    private static AudioSourceController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("AudioSourceController").AddComponent<AudioSource>()
                    .gameObject.AddComponent<AudioSourceController>();
                }

            return _instance; 
        }
    }

    public AudioSource AudioSource;

    private void Awake()
    {
        if(_instance != null)
        {
            throw new System.Exception("Duplicate AudioSourceController");
        }

        if(AudioSource == null)
        {
            AudioSource = GetComponent<AudioSource>(); 
        }

        _instance = this; 
    }

    public static void PlayAudio(AudioClip clip, float volumeScale)
    {
        Instance.AudioSource.PlayOneShot(clip,volumeScale); 
    }

    public static void PlayAudio(AudioClip clip)
    {
        Instance.AudioSource.PlayOneShot(clip);
    }
}
