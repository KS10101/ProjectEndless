using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Instance
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    [SerializeField] private AudioSource BG_Audio;
    [SerializeField] private AudioSource SFX_Audio;


    private void Start()
    {
        PlayBGSound();
        SFX_Audio.loop = false;
    }

    private void PlayBGSound()
    {
        BG_Audio.loop = true;
        if(BG_Audio.clip != null)
            BG_Audio.playOnAwake = true;
    }

    public void PlaySFX(AudioClip clip)
    {
        SFX_Audio.clip = clip;
        SFX_Audio.loop = false;
        SFX_Audio.Play();
    }
}

