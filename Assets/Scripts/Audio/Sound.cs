using UnityEngine;

[System.Serializable]
public class Sound
{
    public string audioName;
    public AudioClip audioClip;

    [Range(0, 1)]
    public float volume;
    public bool isLooping;

    [HideInInspector]
    //public AudioSource audioSource;
    public AudioSource sfxSource;
    [HideInInspector]
    public AudioSource musicSource;
}
