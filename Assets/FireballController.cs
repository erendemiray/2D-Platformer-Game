using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
   [SerializeField] private float speed;
   private float direction;
   private BoxCollider2D boxCollider2D;
   private bool hit;
   private Animator anim;
   private float lifetime;

    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

    }
    void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed,0,0);
        lifetime += Time.deltaTime;

        if(lifetime> 5)
        {
            deactivation();
        }
    }
    private  void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
          hit=true;
        boxCollider2D.enabled = false;
        anim.SetTrigger("explode");  
        }
    }
    public void setDirection(float direction)
    {
        lifetime = 0;
        this.direction = direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider2D.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != direction)
        localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX,transform.localScale.y,transform.localScale.z);

    }

    private void deactivation()
    {
        gameObject.SetActive(false);
    }
}
