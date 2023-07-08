using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Werewolf : MonoBehaviour
{
    [SerializeField] WerewolfUI _WerewolfUI;
    
    public delegate void OnHpChanged(int hp);
    public static event OnHpChanged onHpChanged;

    private int hp;

    void Start()
    {
        PlayerCombat playerCombat = this.gameObject.GetComponent<PlayerCombat>();
        playerCombat.SetDamage(PlayerInfo.Singleton.damage);
        playerCombat.SetRange(PlayerInfo.Singleton.range);
        _WerewolfUI.SetupHealthBar(PlayerInfo.Singleton.hp);
        hp = PlayerInfo.Singleton.hp;
    }

    public void TakeDamage(int amount)
    {
        // hit animation
        hp -= amount;
        if(hp <= 0)
        {
            SceneLoader.Singleton.LoadScene("Menu");
            //get wrekt lmao
        }
        else if(onHpChanged != null)
        {
            onHpChanged(hp);
        }
    }



}
