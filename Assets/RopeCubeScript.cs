using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class RopeCubeScript : MonoBehaviour
{
    private Vector3 initPos;
    private float touchedAt;
    private bool touching;
    private bool cableReleased;
    private bool goodShot;
    private bool okayShot;
    private bool badShot;
    private MeshRenderer cableMeshRenderer;

    private bool playerReadyToBeShot;

    private float rightSideMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        if (this.tag.Equals("right"))
        {
            rightSideMultiplier = -1;
        }
        else
        {
            rightSideMultiplier = 1;
        }
        okayShot = false;
        goodShot = false;
        badShot = false;
        cableReleased = false;
        initPos = transform.position;
        touchedAt = 0;
        cableMeshRenderer = transform.parent.GetComponent<MeshRenderer>();
        playerReadyToBeShot = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if ((Time.realtimeSinceStartup - touchedAt > 2.5f) && touching && !(Time.realtimeSinceStartup - touchedAt > 4.5f) )
        {
            Debug.Log("selam good shot");
            cableMeshRenderer.material.color = Color.green;
            goodShot = true;
            okayShot = false;
        }
        
        if ((Time.realtimeSinceStartup - touchedAt > 4.5f) && touching)
        {
            Debug.Log("selam bad shot");
            cableMeshRenderer.material.color = Color.red;
            goodShot = false;
            badShot = true;
            okayShot = false;
        }
        
        if ((Time.realtimeSinceStartup - touchedAt > 0.5f) && touching && !(Time.realtimeSinceStartup - touchedAt > 2.5f))
        {
            Debug.Log("selam okay shot");
            //cableMeshRenderer.material.color = Color.red;
            goodShot = false;
            badShot = false;
            okayShot = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name.Equals("Player") && Input.GetAxis("Horizontal")*rightSideMultiplier <=0 )
        {
            if (!touching && !cableReleased)
            {
                touchedAt = Time.realtimeSinceStartup;
                touching = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name.Equals("Player") && touching)
        {
            transform.position += Vector3.back*Mathf.Clamp(transform.position.x+(Time.realtimeSinceStartup-touchedAt),0,0.01f)*rightSideMultiplier;
        }

        
        if (Input.GetAxis("Horizontal") == 0 && touching)
        {
            
            transform.DOMove(initPos,0.001f);
            touching = false;
            cableReleased = true;
        }

        if (other.name.Equals("Player") && cableReleased && !touching)
        {
            playerReadyToBeShot = true;
            cableReleased = false;
            
            /*if (goodShot)
            {
                other.transform.GetComponent<Rigidbody>().AddForce(0, 43f,43f);
                cableReleased = false;
            }

            if (badShot)
            {
                other.transform.GetComponent<Rigidbody>().AddForce(0, 60,60);
                cableReleased = false;
            }
            
            if (okayShot)
            {
                other.transform.GetComponent<Rigidbody>().AddForce(0, 45,45);
                cableReleased = false;
            }*/
        }
    }

    private void OnTriggerExit(Collider other)
    {
        /*
        if (other.name.Equals("Player"))
        {
            goodShot = false;
            badShot = false;
            //okayShot = true;
            okayShot = false;
            cableMeshRenderer.material.color = Color.yellow;
        }*/
    }


    public void resetCable()
    {
        goodShot = false;
        badShot = false;
        //okayShot = true;
        okayShot = false;
        cableMeshRenderer.material.color = Color.yellow;
        transform.parent.GetComponent<Obi.ObiRod>().ResetParticles();
    }

    public int whatKindOfShot()
    {
        if (cableMeshRenderer.material.color == Color.green)
        {
            return 1;
        }

        if (cableMeshRenderer.material.color == Color.red)
        {
            return 2;
        }

        if (cableMeshRenderer.material.color == Color.yellow)
        {
            return 0;    
        }

        return -1;
    }

    public bool isPlayerReadyToBeShot()
    {
        return playerReadyToBeShot;
    }

    public void makePlayerNotReadyToBeShot()
    {
        playerReadyToBeShot = false;
    }
    public void releaseCable()
    {
        cableReleased = true;
    }
}
