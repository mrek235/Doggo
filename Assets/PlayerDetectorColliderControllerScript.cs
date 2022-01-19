using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerDetectorColliderControllerScript : MonoBehaviour
{
    private bool notSeenPlayer = true;
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
        if (other.CompareTag("P1") || other.CompareTag("P2") || other.CompareTag("Player"))
        {
            if (notSeenPlayer)
            {
                transform.GetComponentInParent<EnemyController>().SeePlayer(other.transform);
                notSeenPlayer = false;
            }
        }
    }
}
