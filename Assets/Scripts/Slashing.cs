using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slashing : MonoBehaviour
{

    [SerializeField] Transform slashPoint;
    [SerializeField] GameObject swordPrefab;

    public float slashForce = 20f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
            GameObject sword = Instantiate(swordPrefab, slashPoint.position, slashPoint.rotation);
            Rigidbody2D rb = sword.GetComponent<Rigidbody2D>();
            rb.AddForce(slashPoint.up * slashForce, ForceMode2D.Impulse);
    }
}
