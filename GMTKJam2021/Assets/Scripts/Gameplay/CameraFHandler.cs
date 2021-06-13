using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFHandler : MonoBehaviour
{
    public GameObject player;
    public GameObject frontHandler;

    // Update is called once per frame
    void Update()
    {
        if(player && frontHandler)
        {
            transform.position = (player.transform.position + frontHandler.transform.position) / 2;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.GetChild(0).transform.position, (player.transform.position + frontHandler.transform.position) / 2);
    }
}
