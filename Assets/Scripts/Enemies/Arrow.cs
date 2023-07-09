using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField] float arrowForce;
    [SerializeField] int damage;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * arrowForce;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90f);

    }

    // the commented out code makes the arrow home in on the target if we want that
    /*
    void Update()
    {

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * arrowForce;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90f);


    }

    */
    private void OnTriggerEnter2D(Collider2D col)
    {
        col.GetComponent<Werewolf>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
