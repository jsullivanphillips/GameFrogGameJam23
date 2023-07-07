using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Singleton { get; private set; }

    private Animator _Animator;

    void Awake()
    {
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
        StartCoroutine(FadeOut(sceneName));
    }

    private IEnumerator FadeOut(string sceneName)
    {
        // AudioManager audioManager = FindObjectOfType<AudioManager>();
        // audioManager.StopAll();
        // We can stop all music on scene change if we like
        _Animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(sceneName);
    }
}
