using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFHandler : MonoBehaviour
{
    public GameObject player;
    public GameObject frontHandler;
    [SerializeField]
    bool useFrontHandler = false;

    // Update is called once per frame
    void Update()
    {
        if (useFrontHandler && player && frontHandler)
        {
            transform.position = (player.transform.position + frontHandler.transform.position) / 2;
        }
        else if(!useFrontHandler && player)
        {
            transform.position = player.transform.position;
        }
    }
}