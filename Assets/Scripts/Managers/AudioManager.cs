using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource musicSource;
    private AudioSource sfxSource;
    public AudioMixer audioMixer; // Reference to the AudioMixer

    private const string MusicVolumeKey = "musicVolume";
    private const string SFXVolumeKey = "SFXVolume";
    private const float VolumeThreshold = 0.0001f; // Epsilon value to avoid logarithm problems

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Set up two audio sources
            AudioSource[] sources = GetComponents<AudioSource>();
            if (sources.Length >= 2)
            {
                musicSource = sources[0];
                sfxSource = sources[1];
            }
            else
            {
                Debug.LogError("AudioManager needs two AudioSource components!");
            }   
           }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadVolume();
    }

    public void SetVolume(string parameterName, string prefsKey, float sliderValue)
    {
        float volume = sliderValue > VolumeThreshold ? Mathf.Log10(sliderValue) * 20 : -80;
        audioMixer.SetFloat(parameterName, volume);
        PlayerPrefs.SetFloat(prefsKey, sliderValue);
    }

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            float musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey);
            musicSource.volume = musicVolume;    
            SetVolume("music", MusicVolumeKey, musicVolume);
        }

        if (PlayerPrefs.HasKey(SFXVolumeKey))
        {
            float sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey);
            sfxSource.volume = sfxVolume;
            SetVolume("sfx", SFXVolumeKey, sfxVolume);
        }
    }

    public void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip);
    }

}
