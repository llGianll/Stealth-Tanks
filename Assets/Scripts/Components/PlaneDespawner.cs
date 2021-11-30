using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDespawner : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<StealthBomber>())
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<StealthBomber>())
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
