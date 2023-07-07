using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public void OnStartGame()
    {
        
        SceneLoader.Singleton.LoadScene("Game");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
