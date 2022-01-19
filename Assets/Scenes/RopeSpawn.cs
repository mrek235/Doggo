using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RopeSpawn : MonoBehaviour
{
    [SerializeField][Range(1,1000)]private int length = 1;
    public GameObject partPrefab,parentObject;
    [SerializeField] private float partDistance = 0.21f;

    [SerializeField] private bool reset, spawn, snapFirst, snapLast;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            foreach (GameObject tmp in GameObject.FindGameObjectsWithTag("part"))
            {
                Destroy(tmp);
            }

            reset = false;
        }

        if (spawn)
        {
            Spawn();
            spawn = false;
        }
    }


    public void Spawn()
    {
        int count = (int) (length / partDistance);
        for (int x = 0; x < count; x++)
        {
            GameObject tmp;
            tmp = Instantiate(partPrefab, transform.position + Vector3.up*partDistance*(x+1), Quaternion.identity, parentObject.transform);
            tmp.name = parentObject.transform.childCount.ToString();
            tmp.transform.eulerAngles = new Vector3(180, 0, 0); 
            if (x == 0)
            {
                Destroy(tmp.GetComponent<CharacterJoint>());
                if (snapFirst)
                {
                    tmp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }
            else
            {
                tmp.GetComponent<CharacterJoint>().connectedBody =
                    parentObject.transform.Find((parentObject.transform.childCount - 1).ToString())
                        .GetComponent<Rigidbody>();
            }
        }

        if (snapLast)
        {
            parentObject.transform.Find(parentObject.transform.childCount.ToString()).GetComponent<Rigidbody>()
                .constraints = RigidbodyConstraints.FreezeAll;
        }
    }    
}
