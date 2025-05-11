using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public SO_SFXList sfxList;

    public AudioSource sfxAudioSource;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        sfxAudioSource = GetComponentInChildren<AudioSource>();
    }

    public void PlaySFXSound(SFX_Type soundToPlay)
    {
        sfxAudioSource.PlayOneShot(sfxList.GetClip(soundToPlay));
    }
}
