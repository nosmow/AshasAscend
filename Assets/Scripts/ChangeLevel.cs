using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    void Start()
    {

    }


    void Update()
    {

    }
    public void change(int Level1)
    {
        SceneManager.LoadScene(Level1);
    }

}
