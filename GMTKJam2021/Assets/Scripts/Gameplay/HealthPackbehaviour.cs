using GMTK2021.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackbehaviour : MonoBehaviour
{
    public int HealthAmount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerBehavior>().Heal(HealthAmount);
            Destroy(gameObject);
        }
    }
}
