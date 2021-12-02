using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + " fell.");

        IDeath death = other.GetComponent<IDeath>();
        if (death != null)
        {
            death.Death();
        }
        else
        {
            other.gameObject.SetActive(false);
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name + " fell.");

        IDeath death = other.gameObject.GetComponent<IDeath>();
        if (death != null)
        {
            death.Death();
        }
        else
        {
            other.gameObject.SetActive(false);
        }


    }
}
