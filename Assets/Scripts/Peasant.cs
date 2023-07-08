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
    private Vector3 randomPosition;
    private bool canMove = true;
    private int blood = 1;

    [SerializeField] float attackRate;
    float nextAttackTime = 0;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        _Werewolf = GameObject.Find("Werewolf Mayor");
        hp = Random.Range(2, 20);
        Physics2D.IgnoreCollision(_Werewolf.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        _Animator.SetTrigger("IsHit");
        if (hp < 0)
        {
            SpawnManager.Singleton.PeasantHasDied(this.gameObject);
            PlayerInfo.Singleton.blood += blood;
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
        if (IsWerewolfIsClose())
            LookAndMoveTowardsWerewolf();
        else
            MoveRandomDirection();

    }

    bool IsWerewolfIsClose()
    {
        target = _Werewolf.transform.position;
        var delta = target - transform.position;

        if (delta.magnitude < 80)
            return true;
        else 
            return false;
    }


    void MoveRandomDirection()
    {
        if (randomPosition != transform.position)
        {
            if(randomPosition == Vector3.zero)
            {
                float x = Random.Range(-100f, 100f);
                float y = Random.Range(-100f, 100f);
                randomPosition = new Vector3(x, y, 0f);
            }
            target = randomPosition;
            speed = 4f;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            Debug.Log($"Moving towards {randomPosition}");
        }
        else
        {
            float x = Random.Range(-100f, 100f);
            float y = Random.Range(-100f, 100f);
            randomPosition = new Vector3(x, y, 0f);
        }
        
    }

    void LookAndMoveTowardsWerewolf()
    {
        if(canMove)
        {
            target = _Werewolf.transform.position;
            var delta = target - transform.position;

            // if sees the werewolf but far away, go crazy fast
            if (delta.magnitude > 70)
            {
                speed = 20f;
            }
            // if close, attack
            else if (delta.magnitude < 4f)
            {
                speed = 1f;
                if (Time.time >= nextAttackTime)
                {
                    _Animator.SetTrigger("Attacking");
                    nextAttackTime = Time.time + 1f / attackRate;
                }
                    
            }
            // if pretty close slowdown
            else if (delta.magnitude < 20)
            {
                speed = 3f;
            }
            // else just go a normal pace
            else
            {
                speed = 5f;
            }


            float step = speed * Time.deltaTime; 

            Vector3 offset = new Vector3(1.5f * Mathf.Sign(delta.x),1.5f * Mathf.Sign(delta.y), 0f);
            transform.position = Vector3.MoveTowards(transform.position, target - offset, step);

            transform.right = target- transform.position;
        }
    }

}
