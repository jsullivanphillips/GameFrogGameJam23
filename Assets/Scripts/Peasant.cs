using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : MonoBehaviour
{
    private Vector3 _WerewolfLocation;
    private GameObject _Werewolf;

    private int hp = 10;

    [SerializeField] float speed = 5f;
    [SerializeField] Animator _Animator;
    Rigidbody2D rb;
    private Vector3 target;
    int damping = 2;
    private bool canMove = true;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        _Werewolf = GameObject.Find("Werewolf Mayor");
        hp = Random.Range(2, 20);
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        _Animator.SetTrigger("IsHit");
        if (hp < 0)
        {
            SpawnManager.Singleton.PeasantHasDied(this.gameObject);
        }
        else
        {
            StartCoroutine(CanMoveWait());
        }
    }

    private IEnumerator CanMoveWait()
    {
        canMove = false;
        yield return new WaitForSeconds(0.25f);
        canMove = true;
    }


    public void SetHp(int amount)
    {
        hp = amount;
    }


    void Update()
    {
        // make follow only within radiues, otherwise pick random point on map and head to it. need to create groups of peasants.

        LookAndMoveTowardsWerewolf();

    }

    void LookAndMoveTowardsWerewolf()
    {
        if(canMove)
        {
            float step = speed * Time.deltaTime;
            target = _Werewolf.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            Vector3 lookPos = target - transform.position;
            var rotation = Quaternion.LookRotation(lookPos);
            rotation.y = 0;
            rotation.x = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        }
    }

}
