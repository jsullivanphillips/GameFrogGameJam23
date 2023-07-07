using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Werewolf : MonoBehaviour
{
    private int hp = 100;

    [SerializeField] WerewolfUI _WerewolfUI;

    public delegate void OnHpChanged(int hp);
    public static event OnHpChanged onHpChanged;

    void Start()
    {
        _WerewolfUI.SetupHealthBar(hp);
    }

}
