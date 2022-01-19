using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private  Material[] skyboxes;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.Instance.transform;
        int index =  PlayerPrefs.GetInt("_level", 0);
        RenderSettings.skybox = skyboxes[index%skyboxes.Length];
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        float z = player.position.z;
        transform.position = new Vector3(position.x, position.y, z-15f);
    }
}
