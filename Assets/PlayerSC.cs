using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSC : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speedX;
    public float speedY;
    public Transform initialpos;
    private bool grounded = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        transform.position += new Vector3(moveInput.x, 0, 0) * speedX * Time.deltaTime;
        if(moveInput.y > 0 && grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, speedY, 0);

            grounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ground")
        {
            grounded = true;
        }else if(collision.tag == "bounce")
        {
            rb.velocity = new Vector3(rb.velocity.x, 15, 0);
        }else if(collision.tag == "lava")
        {
            transform.position = initialpos.position;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        grounded = false;
    }
}
