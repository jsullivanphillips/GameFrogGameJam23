using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightManager : MonoBehaviour
{
    [SerializeField] Image nightTimer;
    [SerializeField] float nightLengthInSeconds;
    [SerializeField] GameObject nightIsOverParent;
    float currentTime = 0f;

    void Update()
    {
        if(currentTime <= nightLengthInSeconds)
        {
            currentTime += Time.deltaTime;
            float timeElapsed = nightLengthInSeconds - currentTime;
            nightTimer.fillAmount = (timeElapsed / nightLengthInSeconds);
            if(currentTime > nightLengthInSeconds)
            {
                SpawnManager.Singleton.DespawnAllMobs();
                nightIsOverParent.SetActive(true);
            }
        }
        
        
    }

    public void OnReturnToTown()
    {
        SceneLoader.Singleton.LoadScene("Day");
    }
}
