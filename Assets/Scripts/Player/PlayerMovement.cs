using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    private Animator animator;
    private BoxCollider2D boxCollider;
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private LayerMask wallLayer;
    private float horizontalInput;
    private float wallJumpCooldown;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //rigidbodyi playerdan alıyor diğerinde de animatörü alıyor
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
         horizontalInput = Input.GetAxis("Horizontal"); //Hareket inputu

        if(horizontalInput > 0.01f) // Karakteri çevirmek için yapılan işlemler
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f) // Karakteri çevirmek için yapılan işlemler
        {
            transform.localScale = new Vector3 (-1,1,1);
        }

        if(wallJumpCooldown > 0.2f)
        {
            rb.velocity = new Vector2(horizontalInput * speed ,rb.velocity.y);

            if(onWall() && !isGrounded()) //Duvarda mı diye kontrol ediyoruz eğer öyleyse duvardan zıplama mekaniği için karakteri oraya sabitliyoruz
            {
                rb.gravityScale = 0;
                rb.velocity =Vector2.zero;
            }
            else
            {
                rb.gravityScale = 2.5f;
            }

             if (Input.GetKeyDown(KeyCode.Space)) //Zıplama hareketi
            {
            Jump();
            }

           
        }
         else
            {
                wallJumpCooldown += Time.deltaTime;
            }

        animator.SetBool("Run",horizontalInput != 0); // Karakterin hareket inputuna göre animasyonunu belirliyor.
        animator.SetBool("grounded",isGrounded());

    }
    private void Jump()
    {
        if(isGrounded()){
         rb.velocity = new Vector2(rb.velocity.x,jumpSpeed);
        }
        else if(onWall() && !isGrounded())
        {
            if(horizontalInput == 0)
            {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)* 10 , 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x),transform.localScale.y,transform.localScale.z);
            }
            else
            {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)* 3 , 6);
            }
            wallJumpCooldown = 0;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
    private bool isGrounded()
{
    Vector2 size = boxCollider.bounds.size;
    Vector2 center = boxCollider.bounds.center;
    RaycastHit2D raycastHit = Physics2D.BoxCast(center, size, 0f, Vector2.down,0.1f, // Karakterin yere değip değmediğini kontrol etmek için yeterli bir mesafe.
groundLayer
    );
    return raycastHit.collider != null;
}
private bool onWall()
    {
         RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size , 0f, new Vector2 (transform.localScale.x,0),0.1f, wallLayer);
         return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return true;
    }
}
