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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hp -= 5;
            if (onHpChanged != null)
                onHpChanged(hp);
        }
    }
}
