using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;

    float musicVolume =  1; 
        float sfxVolume = 1;

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;

        // Update volume for all currently playing music
        foreach (Sound sound in musicSounds)
        {
            if (sound.musicSource != null)
            {
                sound.musicSource.volume = sound.volume * musicVolume;
            }
        }

    }
    public void SetsfxVolume(float volume)
    {
        sfxVolume = volume;

        // Update volume for all currently playing SFX
        foreach (Sound sound in sfxSounds)
        {
            if (sound.sfxSource != null)
            {
                sound.sfxSource.volume = sound.volume * sfxVolume;
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

        foreach (Sound sound in musicSounds)
        {
            sound.musicSource = gameObject.AddComponent<AudioSource>();
            sound.musicSource.clip = sound.audioClip;
            sound.musicSource.volume = sound.volume;
            sound.musicSource.loop = sound.isLooping;
        }

        foreach (Sound sound in sfxSounds)
        {
            sound.sfxSource = gameObject.AddComponent<AudioSource>();
            sound.sfxSource.clip = sound.audioClip;
            sound.sfxSource.volume = sound.volume;
            sound.sfxSource.loop = sound.isLooping;
        }

        PlayMusic("Storm");
        PlayMusic("WindSea");
        PlayMusic("MainGameMusic");
    }

    public void PlayMusic(string audioName)
    {
        Sound s = Array.Find(musicSounds, musicSounds => musicSounds.audioName == audioName);
        s.musicSource.Play();
    }


    public void PlaySFX(string audioName)
    {
        Sound s = Array.Find(sfxSounds, sfxSounds => sfxSounds.audioName == audioName);
        s.sfxSource.Play();
    }

    public void StopMusic(string audioName)
    {
        Sound s = Array.Find(musicSounds, sound => sound.audioName == audioName);
        if (s != null && s.musicSource.isPlaying)
        {
            s.musicSource.Stop();
        }
    }

    public void StopAllMusic()
    {
        foreach (Sound sound in musicSounds)
        {
            if (sound.musicSource.isPlaying)
            {
                sound.musicSource.Stop();
            }
        }
    }

}