using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WerewolfUI : MonoBehaviour
{
    [SerializeField] Slider hpSlider;

    void Start()
    {
        Werewolf.onHpChanged += OnHpChanged;
    }

    public void SetupHealthBar(int maxHP)
    {
        hpSlider.maxValue = maxHP;
        hpSlider.value = maxHP;
    }

    void OnHpChanged(int hp)
    {
        hpSlider.value = hp;
    }
}
