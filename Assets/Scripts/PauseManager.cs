using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject _PauseCanvas;
    
    void Start()
    {
        GameState.Singleton.onGamePaused += OnPause;
    }

    void OnPause(bool isPaused)
    {
        if (isPaused)
        {
            _PauseCanvas.SetActive(true);
        }
        else
        {
            _PauseCanvas.SetActive(false);
        }
    }

    public void OnBackToMenu()
    {
        GameState.Singleton.onGamePaused -= OnPause;
        GameState.Singleton.SetPause(false);
        SceneLoader.Singleton.LoadScene("Menu");

    }
}
