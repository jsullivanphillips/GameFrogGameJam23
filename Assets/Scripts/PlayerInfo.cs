using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Singleton { get; private set; }

    public int hp = 50;

    public int blood = 0;

    public int damage = 1;

    public int range = 2;

    public float lifesteal = 0;

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
