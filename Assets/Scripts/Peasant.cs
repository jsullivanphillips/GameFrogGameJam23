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
    int damping = 2;
    private bool canMove = true;
    private int blood = 1;

    [SerializeField] float attackRate;
    float nextAttackTime = 0;

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

            if (delta.magnitude > 70)
            {
                speed = 20f;
            }
            else if (delta.magnitude < 5f)
            {
                if (Time.time >= nextAttackTime)
                {
                    _Animator.SetTrigger("Attacking");
                    nextAttackTime = Time.time + 1f / attackRate;
                }
                    
            }
            else if (delta.magnitude < 20)
            {
                speed = 3f;
            }
            else
            {
                speed = 5f;
            }
            float step = speed * Time.deltaTime; // distance * sign of difference of x
            Vector3 offset = new Vector3(1f * Mathf.Sign(delta.x),1f * Mathf.Sign(delta.y), 0f);
            transform.position = Vector3.MoveTowards(transform.position, target - offset, step);

            /*
            var rotation = Quaternion.LookRotation(target);
            rotation.y = 0;
            rotation.x = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            */
            transform.right = target- transform.position;
        }
    }

}
