using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   [SerializeField]private float attackCooldown;
   [SerializeField]private Transform firePoint;
   [SerializeField]private GameObject[] fireballs;
   private Animator animator;
   private PlayerMovement playerMovement;
   private float cooldownTimer = Mathf.Infinity;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

    }

    void Update()
    {
        if(Input.GetMouseButton(0) && attackCooldown < cooldownTimer && playerMovement.canAttack())
        {
           Attack();
        }
        cooldownTimer += Time.deltaTime;
    }
    void Attack()
    {
        animator.SetTrigger("attack");
        cooldownTimer=0;
        fireballs[findFireball()].transform.position = firePoint.position;
        fireballs[findFireball()].GetComponent<FireballController>().setDirection(Mathf.Sign(transform.localScale.x));
    }
    private int findFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if(!fireballs[i].activeInHierarchy)
            return i;
        }
        return 0;
    }

}
