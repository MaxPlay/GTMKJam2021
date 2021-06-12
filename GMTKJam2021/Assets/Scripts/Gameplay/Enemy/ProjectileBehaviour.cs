using GMTK2021;
using GMTK2021.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private Vector3 velocity;

    public GameObject parent;

    public int damage;

    [SerializeField]
    Rigidbody rigidbody;

    public Vector3 Velocity { get => velocity; set {
            velocity = value;
            rigidbody.velocity = value;
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (parent != null && other.gameObject != parent)
        {
            if(other.tag == "Player")
            {
                other.GetComponent<PlayerBehavior>().Damage(damage);
            }
            Destroy(gameObject);
        }
    }
}
