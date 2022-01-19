using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossController : MonoBehaviour
{
    private Animator _animator;
    
    

    private Transform collidedRope;

    private float oldLength;

    [HideInInspector] public bool flown = false;
    //[SerializeField] private Transform startPlace;
    //[SerializeField] private Transform goPlace;
    private Animator animator;
    private Transform goblin;
    public GameManager gameManager;
    private bool startedIdle = false;

    private bool canPatrol = true;

    private bool gameStarted = false;

    private bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        goblin = transform.GetChild(0);
        animator = GetComponentInChildren<Animator>();
        animator.speed = 0.6f;


    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (gameManager.isGameActive && !gameStarted)
            {
                animator.SetTrigger("attack");
                gameStarted = true;
                //GoToEnd();
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
                transform.DOJump(transform.position + Vector3.up * 30,5f,1,5f).OnComplete(()=>transform.gameObject.SetActive(false));
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
        /*
        if (!flown)
        {
            
            if (other.CompareTag("P1"))
            {
                // Debug.Log("goblin is dead");
                ///GetComponent<Rigidbody>().AddForce(0.00001f,0.00001f,0.00001f);
                animator.SetTrigger("attack");
                other.GetComponent<P1ControllerScript>().Die();
                other.transform.parent.GetComponentInChildren<P2ControllerScript>().GetDizzyAndDie();
                other.GetComponentInParent<PlayerController>().Crashed();
            }

            if (other.CompareTag("P2"))
            {
                animator.SetTrigger("attack");
                other.GetComponent<P2ControllerScript>().Die();
                other.transform.parent.GetComponentInChildren<P1ControllerScript>().GetDizzyAndDie();
                other.GetComponent<PlayerController>().Crashed();
            }
            
            if(other.CompareTag("winZone"))
            {
               if (!startedIdle)
                {
                    animator.SetTrigger("idle");
                    startedIdle = false;
                }
            }
        }
        */
        
    }

    private void OnTriggerStay(Collider other)
    {
        /*
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
                other.collider.GetComponent<PlayerController>().Crashed();
            }

            if (other.collider.CompareTag("P2"))
            {
                other.collider.GetComponent<P2ControllerScript>().Die();
                other.collider.transform.parent.GetComponentInChildren<P1ControllerScript>().GetDizzyAndDie();
                other.collider.GetComponent<PlayerController>().Crashed();
            }
        }*/
    }

    /*private void GoToEnd()
    {
        if (!flown)
        {
            transform.DOMove(initGoPlace, 1.4f).OnComplete(() => transform.DORotate(transform.rotation.eulerAngles+Vector3.up*180f,0.2f).OnComplete(()=>GoToStart()));
        }
        
    }

    private void GoToStart()
    {
        if (!flown)
        {
            transform.DOMove(initStartPlace,1.4f).OnComplete(() => transform.DORotate(transform.rotation.eulerAngles+Vector3.up*180f,0.2f).OnComplete(()=>GoToEnd()));
        }
    }*/

 
}
