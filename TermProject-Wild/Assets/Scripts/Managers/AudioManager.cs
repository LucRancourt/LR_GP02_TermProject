using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    // Variables
    [SerializeField] private AudioSource _sfxSource;


    // Functions
    public void PlaySound(SoundEffect sound)
    {
        _sfxSource.clip = sound.Clip;
        _sfxSource.volume = sound.Volume;
        _sfxSource.pitch = sound.Pitch;

        _sfxSource.Play();
    }
}


public class SoundEffect : ScriptableObject
{
    [SerializeField] public AudioClip Clip { get; private set; }
    [SerializeField] public float Volume { get; private set; }
    [SerializeField] public float Pitch { get; private set; }
}