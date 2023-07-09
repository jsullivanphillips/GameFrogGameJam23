using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public void OnStartGame()
    {
        AudioManager.Singleton.Play("StartGame");
        SceneLoader.Singleton.LoadScene("Day");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
