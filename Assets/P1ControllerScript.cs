using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1ControllerScript : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private Animator animator;
    public float force = 1f;
   
    private bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!transform.GetComponentInParent<PlayerController>().isCrashed())
        {
            if (Input.GetKey("a"))
            {
                transform.position += Vector3.left * force;
            }

            if (Input.GetKey("d"))
            {
                transform.position += Vector3.right * force;
            }
        }*/

        if (dead)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
            {
                //Debug.Log("animator stops");
                animator.speed = 0;
            }
        }

    }
    
    public void GetDizzyAndDie()
    {
        if (!dead)
        {
            animator.SetTrigger("dizzy");
            dead = true;
        }
    }

    public void Die()
    {
       // Debug.Log("p1 died");
        if (!dead)
        {
            animator.SetTrigger("die");
            dead = true;
            transform.parent.GetComponent<PlayerController>().gameManager.Fail();
        }
        
    }

}
