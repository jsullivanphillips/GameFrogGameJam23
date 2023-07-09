using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : MonoBehaviour
{
    private Vector3 _WerewolfLocation;
    private GameObject _Werewolf;

    [SerializeField] SpriteRenderer sr1;
    [SerializeField] SpriteRenderer sr2;
    


    [SerializeField] Animator _Animator;
    Rigidbody2D rb;
    private Vector3 target;
    private Vector3 randomPosition;
    private bool canMove = true;
    private int blood = 1;

    [SerializeField] float attackRate;
    float nextAttackTime = 0;

    float speed = 5f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float attackMoveSpeed = 1f;
    [SerializeField] float approachMoveSpeed = 3f;
    [SerializeField] int damage;
    [SerializeField] int hp = 10;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] LayerMask player;
   


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
            NightManager.Singleton.UpdateBloodCount(PlayerInfo.Singleton.blood);
        }
        else
        {
            StartCoroutine(CanMoveWait());
        }
    }

    private IEnumerator CanMoveWait()
    {
        sr1.color = Color.red;
        sr2.color = Color.red;
        canMove = false;
        yield return new WaitForSeconds(0.25f);
        sr1.color = Color.white;
        sr2.color = Color.white;
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

    IEnumerator WaitForAttackAnimation()
    {
        yield return new WaitForSeconds(1f);
        if(boxCollider.IsTouchingLayers(player)){
            _Werewolf.GetComponent<Werewolf>().TakeDamage(damage);
        }
        
        // do attack logic
        // _Werewolf.GetComponent<Werewolf>().TakeDamage(damage);
        // play hooray sound effecT?
        // i.e. AudioManager.Singleton.Play("Hooray");
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
                speed = attackMoveSpeed;
                if (Time.time >= nextAttackTime)
                {
                    _Animator.SetTrigger("Attacking");
                    StartCoroutine(WaitForAttackAnimation());
                    nextAttackTime = Time.time + 1f / attackRate;
                }
                    
            }
            // if pretty close slowdown
            else if (delta.magnitude < 20)
            {
                speed = approachMoveSpeed;
            }
            // else just go a normal pace
            else
            {
                speed = moveSpeed;
            }


            float step = speed * Time.deltaTime; 

            Vector3 offset = new Vector3(1.5f * Mathf.Sign(delta.x),1.5f * Mathf.Sign(delta.y), 0f);
            transform.position = Vector3.MoveTowards(transform.position, target - offset, step);

            transform.right = target- transform.position;
        }
    }

}
