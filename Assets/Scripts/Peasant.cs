using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : MonoBehaviour
{
    private Vector3 _WerewolfLocation;
    private GameObject _Werewolf;

    [SerializeField] float speed = 5f;
    private Vector2 target;

    void Start()
    {
        _Werewolf = GameObject.Find("Werewolf Mayor");
        if (_Werewolf != null)
            Debug.Log("found him!");
    }
    // Find location of werewolf

    // move towards werewolf

    void Update()
    {
        float step = speed * Time.deltaTime;
        target = _Werewolf.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

}
