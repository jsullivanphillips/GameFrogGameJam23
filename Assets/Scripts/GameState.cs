using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Singleton { get; private set; }

    private bool IsPaused;

    public delegate void OnGamePaused(bool paused);
    public event OnGamePaused onGamePaused;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Singleton != null && Singleton != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Singleton = this;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
            if (onGamePaused != null)
                onGamePaused(IsPaused);
        }
    }
}
