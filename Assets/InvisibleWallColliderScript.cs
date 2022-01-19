using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWallColliderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.name.Equals("Player"))
        {
            other.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.transform.name.Equals("Player"))
        {
            /*
            other.transform.GetComponent<Rigidbody>().velocity = new Vector3(
                other.transform.GetComponent<Rigidbody>().velocity.x,
                other.transform.GetComponent<Rigidbody>().velocity.y, 0);*/
            other.transform.position += Vector3.forward * 0.01f;
        }
    }
}
