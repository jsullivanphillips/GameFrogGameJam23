using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] int damage;
    
    [SerializeField] float attackRate;
    float nextAttackTime = 0;

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if(!Input.GetKey(KeyCode.LeftShift))
            {
                if(Input.GetButtonDown("Fire1")) 
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }       
    }

    public void SetDamage(int amount)
    {
        damage = amount;
    }

    public void SetRange(float amount)
    {
        attackRange = amount;
    }

    void Attack()
    {
        anim.SetTrigger("Attacking");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Peasant>().TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
