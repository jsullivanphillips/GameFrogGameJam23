using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Singleton { get; private set; }

    public int blood;

    public int damage;

    public int meleeAttackRange;

    public int rangedAttackRange;

    public float lifesteal;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Singleton != null && Singleton != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Singleton = this;
        }
    }
}
