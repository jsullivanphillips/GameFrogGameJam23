using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Singleton { get; private set; }

    private Animator _Animator;
    private string _CurrentSceneName;
    private string _PreviousSceneName;

    void Awake()
    {
        DontDestroyOnLoad(this);
        if (Singleton != null && Singleton != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Singleton = this;
        }
        _Animator = GetComponent<Animator>();
    }

    public void LoadScene(string sceneName)
    {
        _PreviousSceneName = _CurrentSceneName;
        _CurrentSceneName = sceneName;
        FadeOut();
    }

    private void FadeOut()
    {
        // AudioManager audioManager = FindObjectOfType<AudioManager>();
        // audioManager.StopAll();
        // We can stop all music on scene change if we like
        _Animator.SetTrigger("FadeOut");
    }
}
