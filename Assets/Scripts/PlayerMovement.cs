using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    private Animator animator;
    private bool grounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //rigidbodyi playerdan alıyor diğerinde de animatörü alıyor
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); //Hareket inputu
        rb.velocity = new Vector2(horizontalInput * speed ,rb.velocity.y);

        if(horizontalInput > 0.01f) // Karakteri çevirmek için yapılan işlemler
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f) // Karakteri çevirmek için yapılan işlemler
        {
            transform.localScale = new Vector3 (-1,1,1);
        }

        animator.SetBool("Run",horizontalInput != 0); // Karakterin hareket inputuna göre animasyonunu belirliyor.
        animator.SetBool("grounded",grounded);

        if (Input.GetKey(KeyCode.Space)) //Zıplama hareketi
        {
            Jump();
        }


    }
    private void Jump()
    {
         rb.velocity = new Vector2(rb.velocity.x,jumpSpeed);
         grounded = false;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
