using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;  // Reference to the pause button
    [SerializeField] private GameObject menuPauseButton;  // Reference to the menu pause button

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
}
