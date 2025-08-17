using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioClip musicClip;
    private AudioSource _musicSource;

    private List<AudioSource> _soundEffectSources = new List<AudioSource>();

    void Start()
    {
        InitiateMusic();
    }

    private void InitiateMusic()
    {
        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.clip = musicClip;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    public void PlaySoundEffect(AudioClip clipToPlay)
    {
        AudioSource soundSource = GetAvailableSoundEffectSource();
        soundSource.PlayOneShot(clipToPlay);
    }

    private AudioSource GetAvailableSoundEffectSource()
    {
        foreach (AudioSource soundEffectSource in _soundEffectSources)
        {
            if (!soundEffectSource.isPlaying)
            {
                return soundEffectSource;
            }
        }

        AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
        _soundEffectSources.Add(newAudioSource);
        return newAudioSource;
    }
}


// Failed ScriptableObject attempt -> SO destroys itself without any warning or error
#region SO Attempt
/*
[CreateAssetMenu(fileName = "NewSFXEffect", menuName = "Audio/SFX")]
public class SFX : ScriptableObject
{
    [field: SerializeField] public AudioClip Clip { get; private set; }
    [field: SerializeField, Range(0.0f, 1.0f)] public float Volume { get; private set; } = 1.0f;
    [field: SerializeField, Range(0.1f, 3.0f)] public float Pitch { get; private set; } = 1.0f;
}


public class AudioManager : Singleton<AudioManager>
{
    // Variables
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    private float _musicVolume;
    private float _sfxVolume;

    // Functions

    #region Plays
    public void PlayMusic(SFX music)
    {
        SetupSource(ref _musicSource, _musicVolume, music);

        _musicSource.Play();
    }

    public void PlaySFX(SFX sound)
    {
        SetupSource(ref _sfxSource, _sfxVolume, sound);

        _sfxSource.PlayOneShot(_sfxSource.clip);
    }

    public void PlayRandomSFX(List<SFX> listOfSounds)
    {
        PlaySFX(listOfSounds[Random.Range(0, listOfSounds.Count - 1)]);
    }
    #endregion

    #region Setups
    private void SetupSource(ref AudioSource source, float volumeMultiplier, SFX sfx)
    {
        source.clip = sfx.Clip;
        source.volume = sfx.Volume * volumeMultiplier;
        source.pitch = sfx.Pitch;
    }

    public void SetMusicVolume(float volume)
    {
        _musicVolume = volume / 100.0f;
    }

    public void SetSFXVolume(float volume)
    {
        _sfxVolume = volume / 100.0f;
    }
    #endregion*/
#endregion

