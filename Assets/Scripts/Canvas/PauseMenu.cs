using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;  // Reference to the pause button
    [SerializeField] private GameObject menuPauseButton;  // Reference to the menu pause button


    public Slider volumeSlider;
    public Slider sfxSlider;

    private const string MusicVolumeKey = "musicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    private void Start()
    {
        menuPauseButton.SetActive(false);

        LoadVolume();

        // Event for the slider
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(delegate { ChangeMusicVolume(volumeSlider.value); });
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(sfxSlider.value); });
        }
    }

    // Method to pause the game
    public void Pause()
    {
        Time.timeScale = 0f;
        if (pauseButton != null) 
        {
            pauseButton.SetActive(false);  // Hide the pause button
        }
        if (menuPauseButton != null) 
        {
            menuPauseButton.SetActive(true);  // Show the menu pause button
        }
    }

    // Method to resume the game
    public void Resume()
    {
        Time.timeScale = 1f;
        if (pauseButton != null) 
        {
            pauseButton.SetActive(true);  // Show the pause button
        }
        if (menuPauseButton != null) 
        {
            menuPauseButton.SetActive(false);  // Hide the menu pause button
        }
    }

    // Method to handle both pause and resume based on the game state
    public void TogglePauseResume()
    {
        if (Time.timeScale == 0f)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    private void ChangeMusicVolume(float value)
    {
        AudioManager.Instance.SetVolume("music", MusicVolumeKey, value);
    }

    private void ChangeSFXVolume(float value)
    {
        AudioManager.Instance.SetVolume("sfx", SFXVolumeKey, value);
    }

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            if (volumeSlider != null)
            {
                float musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey);
                volumeSlider.value = musicVolume;
            }
        }

        if (PlayerPrefs.HasKey(SFXVolumeKey))
        {
            if (sfxSlider != null)
            {
                float sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey);
                sfxSlider.value = sfxVolume;
            }
        }
    }
}
