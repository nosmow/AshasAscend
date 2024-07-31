using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider sfxSlider;

    public AudioMixer audioMixer; // Reference to the AudioMixer

    private const string MusicVolumeKey = "musicVolume";
    private const string SFXVolumeKey = "SFXVolume";
    private const float VolumeThreshold = 0.0001f; // Epsilon value to avoid logarithm problems

    void Start()
    {
        LoadVolume();
        // Event for the slider
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(delegate { SetVolume("music", MusicVolumeKey, volumeSlider.value); });
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.AddListener(delegate { SetVolume("sfx", SFXVolumeKey, sfxSlider.value); });
        }
    }

    private void SetVolume(string parameterName, string prefsKey, float sliderValue)
    {
        float volume = sliderValue > VolumeThreshold ? Mathf.Log10(sliderValue) * 20 : -80;
        audioMixer.SetFloat(parameterName, volume);
        PlayerPrefs.SetFloat(prefsKey, sliderValue);
    }

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            if (volumeSlider != null)
            {
                float musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey);
                volumeSlider.value = musicVolume;
                SetVolume("music", MusicVolumeKey, musicVolume);
            } 
        }

        if (PlayerPrefs.HasKey(SFXVolumeKey))
        {
            if (sfxSlider != null)
            {
                float sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey);
                sfxSlider.value = sfxVolume;
                SetVolume("sfx", SFXVolumeKey, sfxVolume);
            }
            
        }
    }
}
