using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NightManager : MonoBehaviour
{
    public static NightManager Singleton { get; private set; }
    [SerializeField] Image nightTimer;
    [SerializeField] float nightLengthInSeconds;
    [SerializeField] GameObject nightIsOverParent;
    [SerializeField] TMP_Text bloodCounterText;
    float currentTime = 0f;

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
        bloodCounterText.text = PlayerInfo.Singleton.blood.ToString();
    }

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

    public void UpdateBloodCount(int amount)
    {
        bloodCounterText.text = amount.ToString();
    }
}
