using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : MonoBehaviour
{
    private Vector3 _WerewolfLocation;
    private GameObject _Werewolf;

    private int hp = 10;

    [SerializeField] float speed = 5f;
    private Vector2 target;

    void Start()
    {
        _Werewolf = GameObject.Find("Werewolf Mayor");
        hp = Random.Range(2, 20);
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp < 0)
        {
            SpawnManager.Singleton.PeasantHasDied(this.gameObject);
        }
    }


    void Update()
    {
        // make follow only within radiues, otherwise pick random point on map and head to it. need to create groups of peasants.
        float step = speed * Time.deltaTime;
        target = _Werewolf.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, target, step);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int damage = Random.Range(1, 3);
            TakeDamage(damage);
        }
    }

}
