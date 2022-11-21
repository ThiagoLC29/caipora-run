using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public PlayerController controller;

    private void Start()
    {
        controller = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerEnter(Collider other) //for trigger things
    {
        if (other.gameObject.tag == "Player")
            controller.Die();
    }

    private void OnCollisionEnter(Collision collision) //for collision things
    {
        if (collision.gameObject.tag == "Player")
            controller.Die();
    }
}
