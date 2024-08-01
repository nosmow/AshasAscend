using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusic : MonoBehaviour
{
    public AudioClip musicScene0;
    public AudioClip musicScene1;
    public AudioClip musicScene2;
    public AudioClip musicScene3;
    public AudioClip musicScene4;
    void Awake()
    {
        //Debug.Log("Awake");
    }

    // called first
    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string scnName = scene.name;
        switch (scnName)
        {   
            case "00_MainMenu":
                AudioManager.Instance.StopMusic();
                AudioManager.Instance.PlayMusic(musicScene0);
                break;
            case "01_TrainingScene":
                AudioManager.Instance.StopMusic();
                AudioManager.Instance.PlayMusic(musicScene1);
                break;
            case "02_Level1":
                AudioManager.Instance.StopMusic();
                AudioManager.Instance.PlayMusic(musicScene2);
                break;
            case "03_Level2":
                AudioManager.Instance.StopMusic();
                AudioManager.Instance.PlayMusic(musicScene3);
                break;
            case "04_Level":
                AudioManager.Instance.StopMusic();
                AudioManager.Instance.PlayMusic(musicScene4);
                break;               
            default:
                break;
        }
    }

    // called third
    void Start()
    {
        //Debug.Log("Start");
    }

    // called when the game is terminated
    void OnDisable()
    {
        //Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}