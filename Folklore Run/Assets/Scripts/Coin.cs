using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public PlayerController controller;

    private void Start()
    {
        controller = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other) //for trigger things
    {
        if (other.gameObject.tag == "Player")
            Debug.Log("you got a coin"); Destroy(gameObject);
        controller.coins++;
    }

}
