using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        AudioManager.Singleton.Play("Theme");
    }

    public void OnStartGame()
    {
        // play button click sound
        SceneLoader.Singleton.LoadScene("Game");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
