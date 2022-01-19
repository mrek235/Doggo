using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Obi;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    float horizontalInput;
    float verticalInput;
    public float divisor;
    public Rigidbody _rigidbody;
    private bool canMove;

    private bool alreadyShot;

    private string lastTag;
    // Start is called before the first frame update
    void Start()
    {
        //_rigidbody = GetComponent<Rigidbody>();
        canMove = false;
        alreadyShot = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("Already shot?" + alreadyShot);
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (canMove)
        {
            
            
            if (horizontalInput > 0)
            {
                transform.position += Vector3.forward / divisor;
                //_rigidbody.AddForce(Vector3.forward);
            }

            if (verticalInput > 0)
            {
                transform.position += Vector3.left / divisor;
                //_rigidbody.AddForce(Vector3.left);
            }


            if (horizontalInput < 0)
            {
                transform.position -= Vector3.forward / divisor;
                //_rigidbody.AddForce(Vector3.back);
            }

            if (verticalInput < 0)
            {
                transform.position -= Vector3.left / divisor;
                //_rigidbody.AddForce(Vector3.right);
            }
        }

    }


    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.name.Equals("Cube"))
        {
            canMove = true;
            if (lastTag == null)
            {
                lastTag = other.collider.tag;
            }
        }

        if (!other.collider.CompareTag(lastTag) && !lastTag.Equals("tagNotSet") && other.collider.name.Equals("Cube"))
        {
            Debug.Log("I make already shot false");
            alreadyShot = false;
            lastTag = other.collider.tag;
            //this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.name.Equals("Cube"))
        {
            canMove = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        int type = other.GetComponent<RopeCubeScript>().whatKindOfShot();
        Debug.Log("the shot type is" + type);
        if (other.name.Equals("ube") && !alreadyShot)
        {
            if (other.GetComponent<RopeCubeScript>().isPlayerReadyToBeShot())
            {
                Debug.Log("player is ready to be shot");
                float rightSideMultiplier = 1f;
                if (other.CompareTag("right"))
                {
                    rightSideMultiplier = -1f;
                }
                if (type == 1)
                {
                    transform.GetComponent<Rigidbody>().AddForce(0, 450f,450f*rightSideMultiplier);
                    //other.GetComponent<RopeCubeScript>().releaseCable();
                    alreadyShot = true;
                    lastTag = other.tag;
                }

                if (type == 2)
                {
                    transform.GetComponent<Rigidbody>().AddForce(0, 1500,1500*rightSideMultiplier);
                    //other.GetComponent<RopeCubeScript>().releaseCable();
                    //other.GetComponent<RopeCubeScript>().resetCable();
                    alreadyShot = true;
                    lastTag = other.tag;
                }
            
                if (type == 0)
                {
                    //transform.GetComponent<Rigidbody>().AddForce(0, 400,400*rightSideMultiplier);
                    //other.GetComponent<RopeCubeScript>().releaseCable();
                    //other.GetComponent<RopeCubeScript>().resetCable();
                    //alreadyShot = true;
                    //lastTag = other.tag;
                }
                
                other.GetComponent<RopeCubeScript>().resetCable();
            }
        }
        
        
    }
}
