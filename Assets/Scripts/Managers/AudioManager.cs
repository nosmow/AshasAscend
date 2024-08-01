using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource audioSource;
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
            audioSource = GetComponent<AudioSource>();
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
            SetVolume("music", MusicVolumeKey, musicVolume);
        }

        if (PlayerPrefs.HasKey(SFXVolumeKey))
        {
            float sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey);
            SetVolume("sfx", SFXVolumeKey, sfxVolume);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
