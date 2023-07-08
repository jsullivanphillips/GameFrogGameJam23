using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DaytimeManager : MonoBehaviour
{
    [SerializeField] TMP_Text numPeasantsConvincedText;
    [SerializeField] TMP_Text numCrossbowsConvincedText;
    [SerializeField] TMP_Text bloodText;

    [SerializeField] TMP_Text currentHpText;
    [SerializeField] TMP_Text currentDamageText;
    [SerializeField] TMP_Text currentLifestealText;
    [SerializeField] TMP_Text currentRangeText;

    [SerializeField] TMP_Text hpCostText;
    [SerializeField] TMP_Text damageCostText;
    [SerializeField] TMP_Text lifestealCostText;
    [SerializeField] TMP_Text rangeCostText;

    int hpCost = 2;
    int damageCost = 2;
    int lifestealCost = 2;
    int rangeCost = 2;

    private int numPeasants;
    private int numCrossbows;

    void Start()
    {
        numPeasants = 1;
        numCrossbows = 1;
        numPeasantsConvincedText.text = $"Num Peasants convinced to hunt : {numPeasants}";
        numCrossbowsConvincedText.text = $"Num Crossbows convinced to hunt : {numCrossbows}";
        bloodText.text = $"Blood: {PlayerInfo.Singleton.blood.ToString()}";
        currentHpText.text = $"Current HP: {PlayerInfo.Singleton.hp}";
        currentDamageText.text = $"Current damage: {PlayerInfo.Singleton.damage}";
        currentLifestealText.text = $"Current lifesteal: {PlayerInfo.Singleton.lifesteal}";
        currentRangeText.text = $"Current range: {PlayerInfo.Singleton.range}";
    }

    public void OnRaiseUnrest()
    {
        numPeasants += Random.Range(1, 6);
        numCrossbows += Random.Range(1, 3);
        numPeasantsConvincedText.text = $"Num Peasants convinced to hunt : {Random.Range(Mathf.Max(numPeasants-5, 1),numPeasants)} - {Random.Range(numPeasants, numPeasants + 5)}";
        numCrossbowsConvincedText.text = $"Num Crossbows convinced to hunt : {Random.Range(Mathf.Max(numCrossbows - 3, 1), numCrossbows)} - {Random.Range(numCrossbows, numCrossbows + 3)}";
    }

    public void OnStartNight()
    {
        GameState.Singleton.numPeasantsToSpawn = numPeasants;
        GameState.Singleton.numCrossbowsToSpawn = numCrossbows;
        SceneLoader.Singleton.LoadScene("Night");
    }

    public void OnUpgradeHp()
    {
        if(PlayerInfo.Singleton.blood >= hpCost)
        {
            PlayerInfo.Singleton.hp += 5;
            PlayerInfo.Singleton.blood -= hpCost;
            currentHpText.text = $"Current HP: {PlayerInfo.Singleton.hp}";
            bloodText.text = $"Blood: {PlayerInfo.Singleton.blood.ToString()}";
            hpCost += 1;
            hpCostText.text = $"{hpCost} blood";
        }
    }

    public void OnUpgradeDamage()
    {
        if (PlayerInfo.Singleton.blood >= damageCost)
        {
            PlayerInfo.Singleton.damage += 1;
            PlayerInfo.Singleton.blood -= damageCost;
            currentDamageText.text = $"Current damage: {PlayerInfo.Singleton.damage}";
            bloodText.text = $"Blood: {PlayerInfo.Singleton.blood.ToString()}";
            damageCost += 1;
            damageCostText.text = $"{damageCost} blood";
        }
    }

    public void OnUpgradeLifesteal()
    {
        if (PlayerInfo.Singleton.blood >= lifestealCost)
        {
            PlayerInfo.Singleton.lifesteal += 0.5f;
            PlayerInfo.Singleton.blood -= lifestealCost;
            currentLifestealText.text = $"Current lifesteal: {PlayerInfo.Singleton.lifesteal}%";
            bloodText.text = $"Blood: {PlayerInfo.Singleton.blood.ToString()}";
            lifestealCost += 1;
            lifestealCostText.text = $"{lifestealCost} blood";
        }
    }

    public void OnUpgradeRange()
    {
        if (PlayerInfo.Singleton.blood >= rangeCost)
        {
            PlayerInfo.Singleton.range += 1;
            PlayerInfo.Singleton.blood -= rangeCost;
            currentRangeText.text = $"Current range: {PlayerInfo.Singleton.range}";
            bloodText.text = $"Blood: {PlayerInfo.Singleton.blood.ToString()}";
            rangeCost += 1;
            rangeCostText.text = $"{rangeCost} blood";
        }
    }


}
