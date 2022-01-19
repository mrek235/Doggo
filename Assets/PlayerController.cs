using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using Lean.Touch;
using Obi;
using Random = System.Random;

public class PlayerController : MonoSingleton<PlayerController>
{

    [SerializeField] private float velocity = 0.1f;
    public GameManager gameManager;
    private bool gotIntoWall = false;

    private int turnCount=1;

    private Animator[] dogAnimators;

    private bool dead = false;

    [SerializeField] private LeanTouch leanTouch;
    [SerializeField] private LeanFingerSwipe leanFingerSwipe;
    private bool inStartingPosition = true;
    private bool onLeft = false;
    private bool onRight = false;
    private bool gameStarted = false;
    private bool levelFinished = false;

    private Vector3 startingPosition;
    private bool swiping = false;
    
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.writeLevelNo();
        gameManager = GameManager.Instance;
        dogAnimators = GetComponentsInChildren<Animator>();
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        inStartingPosition = transform.position.x == startingPosition.x;
        if (gameManager.isGameActive && !levelFinished)
        {
            if (!gotIntoWall && !swiping)
            {
                transform.position += Vector3.forward * (velocity*Time.deltaTime);
            }

            if (!gameStarted){
                foreach (Animator animator in dogAnimators)
                {
                    animator.SetTrigger("run");
                }

                gameStarted = true;
            }
        }
    }

    public void Crashed()
    {
        gotIntoWall = true;
    }

    public bool isCrashed()
    {
        return gotIntoWall;
    }

    private void Die()
    {
        foreach (Animator animator in dogAnimators)
        {
            animator.SetTrigger("die");
        }
    }
    
    /*private void OnCollisionEnter(Collision other)
    {
        if (!gotIntoWall)
        {
            if (other.collider.CompareTag("enemy"))
            {
                other.transform.GetComponentInParent<EnemyController>().flown = true;
                other.transform.position += Vector3.up;
                other.transform.GetComponent<Rigidbody>().AddForce(new Vector3(0,0.0005f,0.0005f), ForceMode.Force);
                other.transform.GetComponent<Rigidbody>().useGravity = false;
                
                Debug.Log("hit enemy");
                GetComponentInChildren<ObiRope>().ResetParticles();
            }
        }
        else
        {
            gameManager.Fail();
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (!gotIntoWall)
        {
            if (other.CompareTag("enemy"))
            {
                other.transform.GetComponentInParent<EnemyController>().flown = true;
                other.transform.position += Vector3.up;
                other.transform.GetComponent<Rigidbody>().AddForce(new Vector3(0.0001f*(new Random().Next(-10,10)),0.0005f*(new Random().Next(-10,10)),0.0005f), ForceMode.Force);
                other.transform.GetComponent<Rigidbody>().useGravity = false;
                
                GetComponentInChildren<ObiRope>().ResetParticles();
            }

            if (other.CompareTag("boss"))
            {
                other.transform.GetComponentInParent<BossController>().flown = true;
                other.transform.position += Vector3.up;
                other.transform.GetComponent<Rigidbody>().AddForce(new Vector3(0.0001f*(new Random().Next(-10,10)),0.0005f*(new Random().Next(-10,10)),0.0005f), ForceMode.Force);
                other.transform.GetComponent<Rigidbody>().useGravity = false;
                
                GetComponentInChildren<ObiRope>().ResetParticles();
            }

            if (other.CompareTag("winZone"))
            {
                gameManager.Win();
                levelFinished = true;
                foreach (Animator dogAnimator in dogAnimators)
                {
                    dogAnimator.SetTrigger("attack02");
                }
            }
        }
        else
        {
            gameManager.Fail();
        }
    }
    public void swipeLeft()
    {
        if (gameManager.isControlsActive)
        {
            if ((transform.localRotation.eulerAngles == Vector3.up * 90f ||
                 transform.localRotation.eulerAngles == Vector3.up * 270f))
            {
                if ((inStartingPosition || onRight) && !onLeft)
                {/*
                    swiping = true;
                    transform.DOMove(transform.position + Vector3.left * 3f + (Vector3.forward * 150f * Time.deltaTime),
                        velocity * Time.deltaTime * 1.5f).OnComplete(() =>
                    {
                        swiping = false;
                    }
                );*/
                    transform.DOMove(
                        transform.position + Vector3.left * 3f + (Vector3.forward * Time.deltaTime*velocity),
                        Time.deltaTime*velocity/4f);
                    if (inStartingPosition)
                    {
                        onLeft = true;
                        //inStartingPosition = false;
                    }
                    else
                    {
                        onLeft = false;
                    }


                    onRight = false; 
                    //inStartingPosition = true;
                    
                }

            }
        }

    }

    public void swipeRight()
    {
        if (gameManager.isControlsActive)
        {
            if ((transform.localRotation.eulerAngles == Vector3.up * 90f ||
                 transform.localRotation.eulerAngles == Vector3.up * 270f))
            {
                if ((inStartingPosition || onLeft) && !onRight)
                {
                    /*
                    swiping = true;
                    transform.DOMove(transform.position + Vector3.right * 3f+(Vector3.forward*150f*Time.deltaTime), velocity*Time.deltaTime*1.5f).OnComplete(() =>
                        {
                            swiping = false;
                        }
                    );*/
                    transform.DOMove(
                        transform.position + Vector3.right * 3f + (Vector3.forward * Time.deltaTime*velocity),
                        Time.deltaTime*velocity/4f);
                    if (inStartingPosition)
                    {
                        onRight = true;
                        //inStartingPosition = false;
                    }
                    else
                    {
                        onRight = false;
                    }

                    onLeft = false; 
                    //inStartingPosition = true;
                    
                }
            }
        }

    }

    public void turnAround()
    {
        if (gameManager.isControlsActive)
        {
            if (!gotIntoWall)
            {
                
                if (!inStartingPosition)
                {
                    Vector3 targetPos = new Vector3(startingPosition.x, transform.position.y, transform.position.z+velocity*Time.deltaTime);
                    transform.DOMove(targetPos, Time.deltaTime);
                    inStartingPosition = true;
                    onLeft = false;
                    onRight = false;
                }

                transform.DORotate(Vector3.up * 90f * turnCount, 0.5f);

                transform.GetChild(0).transform.DOLocalRotate(Vector3.up * -90f * (turnCount % 4F), 0.5f);
                transform.GetChild(1).transform.DOLocalRotate(Vector3.up * -90f * (turnCount % 4F), 0.5f);

                turnCount++;
            }
        }
    }

}
