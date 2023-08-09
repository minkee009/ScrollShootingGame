using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmAudioSource;
    public AudioSource EffectAudioSource;

    public List<AudioClip> bgmAudioClips = new List<AudioClip>();
    public List<AudioClip> explosionAudioClips = new List<AudioClip>();
    public List<AudioClip> itemAudioClips = new List<AudioClip>();

    public static SoundManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        bgmAudioSource.clip = bgmAudioClips[0];
        bgmAudioSource.Play();
    }
}
