using System;
using System.Collections;
using System.Collections.Generic;
using Obi;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    private Vector3 oldSize;

    private float oldLength;
    
    // Start is called before the first frame update
    void Start()
    {
        oldSize = GetComponent<BoxCollider>().size;
    }

    // Update is called once per frame
    void Update()
    {
        //float length = transform.parent.parent.GetComponentInChildren<ObiRope>().CalculateLength();
        float length = transform.parent.parent.GetChild(0).position.x - transform.parent.parent.GetChild(1).position.x;
        GetComponent<BoxCollider>().size = new Vector3(length,GetComponent<BoxCollider>().size.y,GetComponent<BoxCollider>().size.z);
        GetComponent<BoxCollider>().center -= (length-oldLength)*Vector3.right/2f;
        
    }

    void LateUpdate()
    {
        oldSize = GetComponent<BoxCollider>().size;
        oldLength = oldSize.x;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag.Equals("wall"))
        {
            transform.parent.GetComponentInParent<PlayerController>().Crashed();
        }

        if (other.tag.Equals("enemy"))
        {
            other.GetComponent<EnemyController>().getStuck(transform);
        }


    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag.Equals("enemy"))
        {
            other.collider.GetComponent<EnemyController>().getStuck(transform);
        }
        
        
    }
}

