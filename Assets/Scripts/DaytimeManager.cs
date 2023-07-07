using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DaytimeManager : MonoBehaviour
{
    [SerializeField] TMP_Text numPeasantsConvincedText;
    [SerializeField] TMP_Text bloodText;

    [SerializeField] TMP_Text currentHpText;
    [SerializeField] TMP_Text currentDamageText;
    [SerializeField] TMP_Text currentLifestealText;
    [SerializeField] TMP_Text currentRangeText;
    private int numPeasants;
    // Start is called before the first frame update
    void Start()
    {
        numPeasants = 5;
        numPeasantsConvincedText.text = "Num Peasants convinced to hunt : 0";

        bloodText.text = PlayerInfo.Singleton.blood.ToString();
        currentHpText.text = $"Current HP: {PlayerInfo.Singleton.hp}";
        currentDamageText.text = $"Current damage: {PlayerInfo.Singleton.damage}";
        currentLifestealText.text = $"Current lifesteal: {PlayerInfo.Singleton.lifesteal}";
        currentRangeText.text = $"Current range: {PlayerInfo.Singleton.range}";
    }

    public void OnRaiseUnrest()
    {
        numPeasants += Random.Range(1, 6);
        numPeasantsConvincedText.text = $"Num Peasants convinced to hunt : {Random.Range(Mathf.Max(numPeasants-5, 1),numPeasants)} - {Random.Range(numPeasants, numPeasants + 5)}";
    }

    public void OnStartNight()
    {
        GameState.Singleton.numPeasantsToSpawn = numPeasants;
        SceneLoader.Singleton.LoadScene("Night");
    }

    
}
