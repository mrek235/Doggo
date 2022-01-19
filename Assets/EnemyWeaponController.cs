using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("P1"))
        {
            // Debug.Log("goblin is dead");
            ///GetComponent<Rigidbody>().AddForce(0.00001f,0.00001f,0.00001f);
            if (!transform.CompareTag("boss"))
            {
                animator.SetTrigger("attack");
            }

            other.GetComponent<P1ControllerScript>().Die();
            other.transform.parent.GetComponentInChildren<P2ControllerScript>().GetDizzyAndDie();
            other.transform.GetComponentInParent<PlayerController>().Crashed();
        }

        if (other.CompareTag("P2"))
        {
            animator.SetTrigger("attack");
            other.GetComponent<P2ControllerScript>().Die();
            other.transform.parent.GetComponentInChildren<P1ControllerScript>().GetDizzyAndDie();
            other.transform.parent.GetComponent<PlayerController>().Crashed();
        }
        
    }
    
    


}
