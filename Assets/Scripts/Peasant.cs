using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : MonoBehaviour
{
    private Vector3 _WerewolfLocation;
    private GameObject _Werewolf;

    private int hp = 10;
    public bool isDead;

    [SerializeField] float speed = 5f;
    private Vector2 target;

    void Start()
    {
        _Werewolf = GameObject.Find("Werewolf Mayor");
        if (_Werewolf != null)
            Debug.Log("found him!");
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp < 0)
            isDead = true;
    }


    void Update()
    {
        float step = speed * Time.deltaTime;
        target = _Werewolf.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

}
