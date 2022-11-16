using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) //for trigger things
    {
        if (other.gameObject.tag == "Player")
            Debug.Log("hit an obstacle");
    }

    private void OnCollisionEnter(Collision collision) //for collision things
    {
        if (collision.gameObject.tag == "Player")
            Debug.Log("hit an obstacle");
    }
}
