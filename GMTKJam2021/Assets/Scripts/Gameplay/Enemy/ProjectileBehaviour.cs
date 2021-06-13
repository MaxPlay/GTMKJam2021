using GMTK2021;
using GMTK2021.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    enum ProjectileType
    {
        Bullet,
        Fire
    }

    private Vector3 velocity;

    public GameObject parent;

    public int damage;

    [SerializeField]
    new Rigidbody rigidbody;

    [SerializeField]
    ProjectileType type;

    [SerializeField]
    float duration;
    float durationTimer;

    [SerializeField]
    AnimationCurve speedFalloff;

    [SerializeField]
    bool debug = false;

    public Vector3 Velocity { get => velocity; set {
            velocity = value;
            durationTimer = Time.time;
        } 
    }

    private void Update()
    {
        if (Time.time > durationTimer + duration)
        {
            Destroy(gameObject);
        }
        else
            rigidbody.velocity = velocity * speedFalloff.Evaluate((Time.time - durationTimer) / duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (parent != null && other.gameObject != parent)
        {
            if(other.CompareTag("Player") && type == ProjectileType.Bullet)
            {
                other.GetComponent<PlayerBehavior>().Damage(damage);
            }
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if(debug)
        { 
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 1);
            Gizmos.DrawLine(transform.position, transform.position + velocity);
        }
    }
}
