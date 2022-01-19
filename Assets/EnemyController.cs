using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Obi;
using UnityEngine;
using UnityEngine.Networking;
using Random = System.Random;

public class EnemyController : MonoBehaviour
{
    private Transform collidedRope;

    private float oldLength;

    [HideInInspector] public bool flown = false;
    [SerializeField] private Transform startPlace;
    [SerializeField] private Transform goPlace;
    private Animator animator;
    private Transform goblin;
    private Vector3 initStartPlace;
    private Vector3 initGoPlace;
    public GameManager gameManager;
    private bool startedIdle = false;

    private bool canPatrol = true;

    private bool gameStarted = false;

    private bool dead = false;

    private bool didntSeePlayer = true;

    [SerializeField]private float playerSeenCoef = 1f;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        goblin = transform.GetChild(0);
        animator = GetComponentInChildren<Animator>();
        
        initGoPlace = goPlace.position;
        initStartPlace = startPlace.position;

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigid in rigidbodies)
        {
            rigid.useGravity = false;
            rigid.isKinematic = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (gameManager.isGameActive && !gameStarted)
            {
                animator.SetTrigger("move");
                gameStarted = true;
                GoToEnd();
            }

            
        }
        if (flown)
        {
            
            //GetComponent<Rigidbody>().AddForce(Vector3.up*30f+Vector3.forward+Vector3.left*new Random().Next(2));

            if (!dead)
            {
                animator.SetTrigger("die");
                animator.StopPlayback();
                dead = true;
                animator.enabled = false;
                Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
                foreach (Rigidbody rigidbody in rigidbodies)
                {
                    rigidbody.isKinematic = false;
                    rigidbody.useGravity = false;
                    
                }
                //Debug.Log("rigidbody 1 is" + rigidbodies[1].name);
                rigidbodies[1].AddForce(Vector3.forward*500f+Vector3.up*1000f+Vector3.left * (new Random().Next(-100,100)),ForceMode.Impulse);


                //transform.DOJump(transform.position + Vector3.up * 90 + Vector3.left * (new Random().Next(-100,100)),5f,1,5f).OnComplete(()=>transform.gameObject.SetActive(false));
            }

            if (flown)
            {
                if ((initStartPlace.y - transform.position.y) > 3.0F)
                {
                    transform.gameObject.SetActive(false);
                }
            }
            //animator.enabled(false);
        }
        /*
        if (collidedRope)
        {
            float length = transform.parent.parent.GetComponentInChildren<ObiRope>().CalculateLength();
            //transform.position -= Vector3.right * (oldLength - length) / 2;
            //collidedRope.GetComponent<ObiRope>().AddForce(Vector3.back*10f,ForceMode.Impulse);
            //GetComponent<Rigidbody>().AddForce(Vector3.back);
            transform.parent.position += Vector3.back/1500f;
            oldLength = length;
        }*/
    }

    public void getStuck(Transform rope)
    {
        /*Debug.Log(rope.parent.parent.childCount);
        transform.SetParent(rope.parent.parent.GetChild(2).GetChild(0));
        collidedRope = rope.parent.parent.GetChild(2).GetChild(0);
        //collidedRope.GetComponent<ObiRope>().AddForce(Vector3.back*40f,ForceMode.Impulse);
        oldLength = collidedRope.GetComponentInChildren<ObiRope>().CalculateLength();
        transform.DOMove(transform.position-Vector3.back/20f, 1f);
        //transform.parent.position += Vector3.back;*/

    }


    private void OnTriggerEnter(Collider other)
    {
        if (!flown)
        {/*
            if (other.CompareTag("P1"))
            {
                // Debug.Log("goblin is dead");
                ///GetComponent<Rigidbody>().AddForce(0.00001f,0.00001f,0.00001f);
                animator.SetTrigger("attack");
                other.GetComponent<P1ControllerScript>().Die();
                other.transform.parent.GetComponentInChildren<P2ControllerScript>().GetDizzyAndDie();
                other.transform.parent.GetComponent<PlayerController>().Crashed();
            }

            if (other.CompareTag("P2"))
            {
                animator.SetTrigger("attack");
                other.GetComponent<P2ControllerScript>().Die();
                other.transform.parent.GetComponentInChildren<P1ControllerScript>().GetDizzyAndDie();
                other.transform.parent.GetComponent<PlayerController>().Crashed();
            }
            
            if(other.CompareTag("winZone"))
            {
                if (!startedIdle)
                {
                    animator.SetTrigger("idle");
                    startedIdle = false;
                }
            }*/
        }
        
        
    }

    private void OnTriggerStay(Collider other)
    {/*
        if (!flown)
        {
            if(other.CompareTag("winZone"))
            {
                if (!startedIdle)
                {
                    animator.SetTrigger("idle");
                    startedIdle = false;
                }
            }
        }*/
        
    }

    private void OnCollisionEnter(Collision other)
    {/*
        if (!flown)
        {
            if (other.collider.CompareTag("P1") || other.collider.CompareTag("P2"))
            {
                // Debug.Log("goblin is dead");
                ///GetComponent<Rigidbody>().AddForce(0.00001f,0.00001f,0.00001f);
                animator.SetTrigger("attack");
                other.collider.transform.parent.GetComponent<PlayerController>().Crashed();
            }

            if (other.collider.CompareTag("P1"))
            {
                // Debug.Log("goblin is dead");
                ///GetComponent<Rigidbody>().AddForce(0.00001f,0.00001f,0.00001f);

                other.collider.GetComponent<P1ControllerScript>().Die();
                other.collider.transform.parent.GetComponentInChildren<P2ControllerScript>().GetDizzyAndDie();
                //other.collider.GetComponent<PlayerController>().crashed();
            }

            if (other.collider.CompareTag("P2"))
            {
                other.collider.GetComponent<P2ControllerScript>().Die();
                other.collider.transform.parent.GetComponentInChildren<P1ControllerScript>().GetDizzyAndDie();
                //other.collider.GetComponent<PlayerController>().crashed();
            }
        }*/
    }

    private void GoToEnd()
    {
        if (!flown)
        {
            transform.DOMove(initGoPlace, 1.4f*playerSeenCoef).OnComplete(() => transform.DORotate(transform.rotation.eulerAngles+Vector3.up*180f,0.2f).OnComplete(()=>GoToStart()));
        }
        
    }

    private void GoToStart()
    {
        if (!flown)
        {
            transform.DOMove(initStartPlace,1.4f*playerSeenCoef).OnComplete(() => transform.DORotate(transform.rotation.eulerAngles+Vector3.up*180f,0.2f).OnComplete(()=>GoToEnd()));
        }
    }


    public void SeePlayer(Transform player)
    {
        didntSeePlayer = false;
        initGoPlace = player.position;
        initStartPlace = transform.position;
        playerSeenCoef = 3f;
    }

    
}