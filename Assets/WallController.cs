using System;
using System.Collections;
using System.Collections.Generic;
using Obi;
using UnityEngine;

public class WallController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("P1")||other.CompareTag("P2")||other.CompareTag("Player"))
        {
            // Debug.Log("goblin is dead");
            ///GetComponent<Rigidbody>().AddForce(0.00001f,0.00001f,0.00001f);
            other.transform.GetComponent<PlayerController>().Crashed();
        }
        if (other.CompareTag("P1"))
        {
            // Debug.Log("goblin is dead");
            ///GetComponent<Rigidbody>().AddForce(0.00001f,0.00001f,0.00001f);
            other.GetComponent<P1ControllerScript>().Die();
            other.transform.parent.GetChild(1).GetComponent<P2ControllerScript>().GetDizzyAndDie();
            //other.collider.GetComponent<PlayerController>().crashed();
        }

        if (other.CompareTag("P2"))
        {
            other.GetComponent<P2ControllerScript>().Die();
            other.transform.parent.GetChild(0).GetComponent<P1ControllerScript>().GetDizzyAndDie();
            //other.collider.GetComponent<PlayerController>().crashed();
        }
        if(other.CompareTag("Player"))
        {
            other.GetComponentInChildren<P1ControllerScript>().GetDizzyAndDie();
            other.GetComponentInChildren<P2ControllerScript>().GetDizzyAndDie();
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("P1")||other.collider.CompareTag("P2"))
        {
            // Debug.Log("goblin is dead");
            ///GetComponent<Rigidbody>().AddForce(0.00001f,0.00001f,0.00001f);
            other.transform.parent.GetComponent<PlayerController>().Crashed();
        }
        if (other.collider.CompareTag("P1"))
        {
            // Debug.Log("goblin is dead");
            ///GetComponent<Rigidbody>().AddForce(0.00001f,0.00001f,0.00001f);
            other.collider.GetComponent<P1ControllerScript>().Die();
            other.transform.parent.GetChild(1).GetComponent<P2ControllerScript>().GetDizzyAndDie();
            //other.collider.GetComponent<PlayerController>().crashed();
        }

        if (other.collider.CompareTag("P2"))
        {
            other.collider.GetComponent<P2ControllerScript>().Die();
            other.transform.parent.GetChild(0).GetComponent<P1ControllerScript>().GetDizzyAndDie();
            //other.collider.GetComponent<PlayerController>().crashed();
        }
    }
    
    
}
