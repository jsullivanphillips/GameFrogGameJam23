using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfMovement : MonoBehaviour
{
    Rigidbody2D body;
    float horizontal;
    float vertical;
    public Camera cam;

    [SerializeField] private float runSpeed;
    Vector2 mousePos;

    void Start ()
    {
        body = GetComponent<Rigidbody2D>(); 
    }

    void Update ()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {  
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

        Vector2 lookDir = mousePos - body.position; 
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        body.rotation = angle;
    }
}
